using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Ringen.CrossCutting.Helpers
{
    public static class PasswordHelper
    {
        static byte[] entropy = Encoding.Unicode.GetBytes("#5!b.i#ivd9pdSc*zSgJ8Sv2w#o:CrM@Fgy");

        public static string EncryptString(SecureString input)
        {
            byte[] encryptedData = ProtectedData.Protect(
                Encoding.Unicode.GetBytes(ToInsecureString(input)),
                entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        public static SecureString DecryptString(string encryptedData)
        {
            if (string.IsNullOrEmpty(encryptedData))
                return new SecureString();

            byte[] decryptedData = ProtectedData.Unprotect(
                Convert.FromBase64String(encryptedData),
                entropy,
                DataProtectionScope.CurrentUser);

            return ToSecureString(Encoding.Unicode.GetString(decryptedData));
        }

        public static SecureString ToSecureString(string input)
        {
            SecureString secure = new SecureString();
            foreach (char c in input)
            {
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();
            return secure;
        }

        public static string ToInsecureString(SecureString input)
        {
            if (input == null)
                return string.Empty;

            string returnValue = string.Empty;
            IntPtr ptr = Marshal.SecureStringToBSTR(input);
            try
            {
                returnValue = Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }
    }
}
