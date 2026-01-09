using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Interop.Tests
{
    [TestClass]
    public class ApiHelperTester
    {
        [TestMethod]
        public void ApiHelper_Validate_Success()
        {
            // Should not throw
            ApiHelper.Validate(HResult.Success);
        }

        [TestMethod]
        public void ApiHelper_Validate_Cancelled()
        {
            Assert.ThrowsExactly<OperationCanceledException>(() =>
            {
                ApiHelper.Validate(HResult.ActionCancelled);
            });
        }

        [TestMethod]
        public void ApiHelper_Validate_OtherError()
        {
            Assert.ThrowsExactly<Win32Exception>(() =>
            {
                ApiHelper.Validate(HResult.KeyStorageFull);
            });
        }
    }
}
