using System.Security.Cryptography;
using System.Text;

namespace BuildMonitor.UI.Protection
{
    internal static class ProtectionMethods
    {
        public static ProtectedInformation Protect(string plaintext)
        {
            // Generate additional entropy (will be used as the Initialization vector)
            var entropy = RandomNumberGenerator.GetBytes(20);
            var plainbytes = Encoding.Unicode.GetBytes(plaintext);
            var cipherbytes = ProtectedData.Protect(plainbytes, entropy, DataProtectionScope.CurrentUser);

            return new ProtectedInformation(cipherbytes, entropy);
        }

        public static string Unprotect(ProtectedInformation protectedData)
        {
            var plainbytes = ProtectedData.Unprotect(protectedData.DataCipherBytes, protectedData.DataEntropyBytes, DataProtectionScope.CurrentUser);

            return Encoding.Unicode.GetString(plainbytes);
        }
    }
}
