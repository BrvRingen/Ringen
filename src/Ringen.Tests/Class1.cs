using System;
using System.Net;
using System.Security;
using NUnit.Framework;
using Ringen.CrossCutting.Helpers;

namespace Ringen.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Encryption_Test()
        {
            var verschluesselt = PasswordHelper.EncryptString(new NetworkCredential("", "Test").SecurePassword);
        }
    }
}
