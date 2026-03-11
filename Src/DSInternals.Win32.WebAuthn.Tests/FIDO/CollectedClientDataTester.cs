using System;
using System.Text.Json;
using DSInternals.Win32.WebAuthn;
using DSInternals.Win32.WebAuthn.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.FIDO.Tests
{
    [TestClass]
    public class CollectedClientDataTester
    {
        // TODO: Perform serialization tests based on https://www.w3.org/TR/webauthn/#clientdatajson-verification

        [TestMethod]
        public void CollectedClientData_Serialize_Vector1()
        {
            var input = new CollectedClientData()
            {
                Type = ApiConstants.ClientDataCredentialCreate,
                Challenge = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 },
                Origin = "https://login.microsoft.com"
            };

            string expected = $@"{{""type"":""{ApiConstants.ClientDataCredentialCreate}"",""challenge"":""AQIDBAU"",""origin"":""https://login.microsoft.com"",""crossOrigin"":false}}";

            string clientDataJson = JsonSerializer.Serialize(input, WebAuthnJsonContext.Default.CollectedClientData);
            Assert.AreEqual(expected, clientDataJson);

            byte[]? clientDataBinary;
            using (var nativeClientData = new ClientData(input))
            {
                clientDataBinary = nativeClientData.ClientDataRaw;
            }
            // TODO: Test the binary value
        }

        [TestMethod]
        public void CollectedClientData_Deserialize_Vector1()
        {
            string clientDataJson = $@"{{""type"":""{ApiConstants.ClientDataCredentialCreate}"",""challenge"":""FsxBWwUb1jOFRA3ILdkCsPdCZkzohvd3JrCNeDqWpJQ"",""origin"":""http://localhost:8080""}}";
            var clientData = JsonSerializer.Deserialize(clientDataJson, WebAuthnJsonContext.Default.CollectedClientData);
            Assert.IsNotNull(clientData);

            Assert.AreEqual("http://localhost:8080", clientData.Origin);
            Assert.AreEqual(ApiConstants.ClientDataCredentialCreate, clientData.Type);
            Assert.IsFalse(clientData.CrossOrigin);
            CollectionAssert.AreEqual(Convert.FromBase64String("FsxBWwUb1jOFRA3ILdkCsPdCZkzohvd3JrCNeDqWpJQ="), clientData.Challenge);
        }

        [TestMethod]
        public void CollectedClientData_Deserialize_Vector2()
        {
            string clientDataJson = "{\"challenge\":\"qNqrdXUrk5S7dCM1MAYH3qSVDXznb-6prQoGqiACR10\",\"origin\":\"https://demo.yubico.com\",\"type\":\"" + ApiConstants.ClientDataCredentialCreate + "\"}";
            var clientData = JsonSerializer.Deserialize(clientDataJson, WebAuthnJsonContext.Default.CollectedClientData);
            Assert.IsNotNull(clientData);
            Assert.AreEqual("https://demo.yubico.com", clientData.Origin);
            Assert.IsFalse(clientData.CrossOrigin);
            Assert.AreEqual(ApiConstants.ClientDataCredentialCreate, clientData.Type);
            CollectionAssert.AreEqual(Convert.FromBase64String("qNqrdXUrk5S7dCM1MAYH3qSVDXznb+6prQoGqiACR10="), clientData.Challenge);
        }
    }
}
