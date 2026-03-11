using System;
using System.Collections.ObjectModel;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;
using Fido2NetLib;
using Fido2NetLib.Objects;

namespace DSInternals.Win32.WebAuthn.Adapter
{
    /// <summary>
    /// Adapter that bridges Fido2NetLib request/response models to the WebAuthn interop API.
    /// </summary>
    public class WebAuthnApiAdapter
    {
        private WebAuthnApi _api;

        /// <summary>
        /// Initializes a new instance of the adapter.
        /// </summary>
        public WebAuthnApiAdapter()
        {
            _api = new WebAuthnApi();
        }

        /// <summary>
        /// Creates a public key credential source bound to a managing authenticator.
        /// </summary>
        /// <param name="options">Options for credential creation.</param>
        /// <returns>The credential public key associated with the credential private key.</returns>
        public AuthenticatorAttestationRawResponse AuthenticatorMakeCredential(CredentialCreateOptions options)
        {
            ArgumentNullException.ThrowIfNull(options);

            var rp = ApiMapper.Translate(options.Rp);
            var credParams = ApiMapper.Translate(options.PubKeyCredParams);
            var excludeCreds = ApiMapper.Translate(options.ExcludeCredentials);
            uint timeout = checked((uint)options.Timeout);
            var uv = ApiMapper.Translate(options.AuthenticatorSelection?.UserVerification);
            var rk = ApiMapper.TranslateResidentKey(options.AuthenticatorSelection?.ResidentKey);
            var attachment = ApiMapper.Translate(options.AuthenticatorSelection?.AuthenticatorAttachment);
            var attestationPref = ApiMapper.Translate(options.Attestation);
            var user = ApiMapper.Translate(options.User);

            var attestation = _api.AuthenticatorMakeCredential(
                rp,
                user,
                options.Challenge,
                uv,
                attachment,
                rk,
                credParams,
                attestationPref,
                timeout,
                new ReadOnlyCollection<PublicKeyCredentialDescriptor>(excludeCreds)
            );

            var attestationResponse = attestation.Response as AuthenticatorAttestationResponse
                ?? throw new InvalidOperationException("Unexpected attestation response type.");

            return new AuthenticatorAttestationRawResponse()
            {
                // TODO: Id = attestation.CredentialId,
                // TODO: RawId = attestation.CredentialId,
                Type = PublicKeyCredentialType.PublicKey,
                // TODO: Extensions = ApiMapper.Translate(attestation.Extensions),
                Response = new AuthenticatorAttestationRawResponse.AttestationResponse()
                {
                    AttestationObject = attestationResponse.AttestationObject,
                    ClientDataJson = attestation.Response.ClientDataJson
                }
            };
        }

        /// <summary>
        /// Asynchronously creates a public key credential source bound to a managing authenticator.
        /// </summary>
        /// <param name="options">Options for credential creation.</param>
        /// <param name="cancellationToken">Cancellation token used to cancel the operation.</param>
        /// <returns>The credential public key associated with the credential private key.</returns>
        public async Task<AuthenticatorAttestationRawResponse> AuthenticatorMakeCredentialAsync(CredentialCreateOptions options, CancellationToken cancellationToken = default)
        {
            cancellationToken.Register(state => CancelCurrentOperation(), null, false);
            return await Task.Run(() => AuthenticatorMakeCredential(options), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Signs a challenge and other collected data into an assertion, which is used as a credential.
        /// </summary>
        /// <param name="options">Assertion options.</param>
        /// <param name="authenticatorAttachment">Optionally filters the eligible authenticators by their attachment.</param>
        /// <returns>The cryptographically signed Authenticator Assertion Response object returned by an authenticator.</returns>
        public AuthenticatorAssertionRawResponse AuthenticatorGetAssertion(AssertionOptions options, Fido2NetLib.Objects.AuthenticatorAttachment? authenticatorAttachment = null)
        {
            ArgumentNullException.ThrowIfNull(options);
            if (string.IsNullOrWhiteSpace(options.RpId))
            {
                throw new ArgumentException("RP ID must be provided.", nameof(options));
            }

            var allowCreds = ApiMapper.Translate(options.AllowCredentials);
            uint timeout = checked((uint)options.Timeout);
            var attachment = ApiMapper.Translate(authenticatorAttachment);
            var uv = ApiMapper.Translate(options.UserVerification);
            // TODO: U2fAppId = options.Extensions?.AppID
            //    if(options.Extensions?.AppID != null)
            //    {
            //        clientData.Add("ClientExtensions", new
            //        {
            //            AppId = options.Extensions.AppID
            //        });
            //    }

            var assertion = _api.AuthenticatorGetAssertion(
                options.RpId,
                options.Challenge,
                uv,
                attachment,
                timeout,
                new ReadOnlyCollection<PublicKeyCredentialDescriptor>(allowCreds)
            );

            var assertionResponse = assertion.Response as AuthenticatorAssertionResponse
                ?? throw new InvalidOperationException("Unexpected assertion response type.");

            return new AuthenticatorAssertionRawResponse()
            {
                // TODO: Id = assertion..Credential.Id,
                // TODO: RawId = assertion.Credential.Id,
                Type = PublicKeyCredentialType.PublicKey,
                Response = new AuthenticatorAssertionRawResponse.AssertionResponse()
                {
                    AuthenticatorData = assertionResponse.AuthenticatorData,
                    Signature = assertionResponse.Signature,
                    UserHandle = assertionResponse.UserHandle,
                    ClientDataJson = assertion.Response.ClientDataJson
                },
            };
        }

        /// <summary>
        /// Asynchronously signs a challenge and collected data into an assertion.
        /// </summary>
        /// <param name="options">Assertion options.</param>
        /// <param name="authenticatorAttachment">Optionally filters eligible authenticators by attachment.</param>
        /// <param name="cancellationToken">Cancellation token used to cancel the operation.</param>
        /// <returns>The cryptographically signed authenticator assertion response.</returns>
        public async Task<AuthenticatorAssertionRawResponse> AuthenticatorGetAssertionAsync(AssertionOptions options, Fido2NetLib.Objects.AuthenticatorAttachment? authenticatorAttachment = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.Register(state => CancelCurrentOperation(), null, false);
            return await Task.Run(() => AuthenticatorGetAssertion(options, authenticatorAttachment), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Requests cancellation of the currently running WebAuthn operation.
        /// </summary>
        public void CancelCurrentOperation()
        {
            _api.CancelCurrentOperation();
        }
    }
}
