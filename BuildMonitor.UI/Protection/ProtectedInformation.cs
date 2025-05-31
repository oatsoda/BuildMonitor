using System;

namespace BuildMonitor.UI.Protection
{
    public class ProtectedInformation
    {
        public string DataCipher { get; private set; }
        public string DataEntropy { get; private set; }

        public byte[] DataCipherBytes
        {
            get { return Convert.FromBase64String(DataCipher ?? string.Empty); }
            private set { DataCipher = Convert.ToBase64String(value); }
        }

        public byte[] DataEntropyBytes
        {
            get { return Convert.FromBase64String(DataEntropy ?? string.Empty); }
            private set { DataEntropy = Convert.ToBase64String(value); }
        }

        public ProtectedInformation(byte[] dataCipher, byte[] dataEntropy)
        {
            DataEntropyBytes = dataEntropy;
            DataCipherBytes = dataCipher;
        }

        public ProtectedInformation(string dataCipher, string dataEntropy)
        {
            DataEntropy = dataEntropy;
            DataCipher = dataCipher;
        }
    }
}