using System.Security.Cryptography;
using System.Text;

namespace BuildMonitor.UI
{
    internal static class ProtectionMethods
    {
        public static ProtectedInformation Protect(string plaintext)
        {
            // Generate additional entropy (will be used as the Initialization vector)
            var entropy = new byte[20];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(entropy);
            }

            var plainbytes = Encoding.Unicode.GetBytes(plaintext);

            var cipherbytes = ProtectedData.Protect(plainbytes, entropy, DataProtectionScope.CurrentUser);

            return new ProtectedInformation(cipherbytes, entropy);
        }

        public static string Unprotect(ProtectedInformation protectedData)
        {
            var plainbytes = ProtectedData.Unprotect(protectedData.DataHashBytes, protectedData.DataEntropyBytes, DataProtectionScope.CurrentUser);

            return Encoding.Unicode.GetString(plainbytes);
        }
    }
}
