using System;
using System.Globalization;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using DSInternals.Win32.WebAuthn.COSE;
using PeterO.Cbor;

namespace DSInternals.Win32.WebAuthn.FIDO
{
    /// <summary>
    /// Represents a parsed COSE <c>credentialPublicKey</c> structure.
    /// </summary>
    public class CredentialPublicKey
    {
        private CBORObject _cpk;

        /// <summary>
        /// Gets the COSE key type.
        /// </summary>
        public KeyType Type
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the COSE signature algorithm identifier.
        /// </summary>
        public Algorithm Algorithm
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the RSA public key when the key type is RSA.
        /// </summary>
        public RSACng? RSA
        {
            get
            {
                if (Type == COSE.KeyType.RSA)
                {
                    var rsa = new RSACng();
                    rsa.ImportParameters(
                        new RSAParameters()
                        {
                            Modulus = _cpk[CBORObject.FromObject(KeyTypeParameter.N)].GetByteString(),
                            Exponent = _cpk[CBORObject.FromObject(KeyTypeParameter.E)].GetByteString()
                        }
                    );
                    return rsa;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the ECDSA public key when the key type is EC2.
        /// </summary>
        public ECDsa? ECDsa
        {
            get
            {
                if (Type == KeyType.EC2)
                {
                    var point = new ECPoint
                    {
                        X = _cpk[CBORObject.FromObject(KeyTypeParameter.X)].GetByteString(),
                        Y = _cpk[CBORObject.FromObject(KeyTypeParameter.Y)].GetByteString(),
                    };
                    ECCurve curve;
                    var crv = (EllipticCurve)_cpk[CBORObject.FromObject(KeyTypeParameter.Crv)].AsInt32();
                    switch (Algorithm)
                    {
                        case Algorithm.ES256:
                            switch (crv)
                            {
                                case COSE.EllipticCurve.P256:
                                case COSE.EllipticCurve.P256K:
                                    curve = ECCurve.NamedCurves.nistP256;
                                    break;
                                default:
                                    throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Missing or unknown crv {0}", crv.ToString()));
                            }
                            break;
                        case Algorithm.ES384:
                            switch (crv) // https://www.iana.org/assignments/cose/cose.xhtml#elliptic-curves
                            {
                                case EllipticCurve.P384:
                                    curve = ECCurve.NamedCurves.nistP384;
                                    break;
                                default:
                                    throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Missing or unknown crv {0}", crv.ToString()));
                            }
                            break;
                        case Algorithm.ES512:
                            switch (crv) // https://www.iana.org/assignments/cose/cose.xhtml#elliptic-curves
                            {
                                case EllipticCurve.P521:
                                    curve = ECCurve.NamedCurves.nistP521;
                                    break;
                                default:
                                    throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Missing or unknown crv {0}", crv.ToString()));
                            }
                            break;
                        default:
                            throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Missing or unknown alg {0}", Algorithm.ToString()));
                    }
                    return ECDsa.Create(new ECParameters
                    {
                        Q = point,
                        Curve = curve
                    });
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the RSA signature padding implied by the selected algorithm.
        /// </summary>
        public RSASignaturePadding? Padding
        {
            get
            {
                if (Type == KeyType.RSA)
                {
                    switch (Algorithm) // https://www.iana.org/assignments/cose/cose.xhtml#algorithms
                    {
                        case Algorithm.PS256:
                        case Algorithm.PS384:
                        case Algorithm.PS512:
                            return RSASignaturePadding.Pss;

                        case Algorithm.RS1:
                        case Algorithm.RS256:
                        case Algorithm.RS384:
                        case Algorithm.RS512:
                            return RSASignaturePadding.Pkcs1;
                        default:
                            throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Missing or unknown alg {0}", Algorithm.ToString()));
                    }
                }

                // This is not a RSA key, so there is no padding.
                return null;
            }
        }

        /// <summary>
        /// Gets the EdDSA public key bytes when the key type is OKP and algorithm is EdDSA.
        /// </summary>
        public byte[]? EdDSAPublicKey
        {
            get
            {
                if (Type == KeyType.OKP)
                {
                    switch (Algorithm) // https://www.iana.org/assignments/cose/cose.xhtml#algorithms
                    {
                        case COSE.Algorithm.EdDSA:
                            var crv = (COSE.EllipticCurve)_cpk[CBORObject.FromObject(KeyTypeParameter.Crv)].AsInt32();
                            switch (crv) // https://www.iana.org/assignments/cose/cose.xhtml#elliptic-curves
                            {
                                case COSE.EllipticCurve.Ed25519:
                                    var publicKey = _cpk[CBORObject.FromObject(KeyTypeParameter.X)].GetByteString();
                                    return publicKey;
                                default:
                                    throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Missing or unknown crv {0}", crv.ToString()));
                            }
                        default:
                            throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Missing or unknown alg {0}", Algorithm.ToString()));
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Initializes a new instance from a CBOR credential public key object.
        /// </summary>
        /// <param name="cpk">CBOR representation of <c>credentialPublicKey</c>.</param>
        public CredentialPublicKey(CBORObject cpk)
        {
            _cpk = cpk ?? throw new ArgumentNullException(nameof(cpk));
            this.Type = (KeyType)cpk[CBORObject.FromObject(KeyCommonParameter.KeyType)].AsInt32();
            this.Algorithm = (Algorithm)cpk[CBORObject.FromObject(KeyCommonParameter.Alg)].AsInt32();
        }

        /// <summary>
        /// Returns a textual representation of the underlying CBOR object.
        /// </summary>
        /// <returns>CBOR diagnostic notation string.</returns>
        public override string ToString()
        {
            return _cpk.ToString();
        }

        /// <summary>
        /// Encodes the credential public key to raw CBOR bytes.
        /// </summary>
        /// <returns>CBOR-encoded key bytes.</returns>
        public byte[] GetBytes()
        {
            return _cpk.EncodeToBytes();
        }
    }
}
