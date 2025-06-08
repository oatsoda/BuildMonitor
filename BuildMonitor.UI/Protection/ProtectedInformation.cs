using System;

namespace BuildMonitor.UI.Protection
{
    public class ProtectedInformation
    {
        public string DataCipher { get; private set; }
        public string DataEntropy { get; private set; }

        public byte[] DataCipherBytes => Convert.FromBase64String(DataCipher);
        public byte[] DataEntropyBytes => Convert.FromBase64String(DataEntropy);

        public ProtectedInformation(byte[] dataCipher, byte[] dataEntropy)
        {
            DataCipher = Convert.ToBase64String(dataCipher);
            DataEntropy = Convert.ToBase64String(dataEntropy);
        }

        public ProtectedInformation(string dataCipher, string dataEntropy)
        {
            DataCipher = dataCipher;
            DataEntropy = dataEntropy;
        }
    }
}