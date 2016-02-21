using System;

namespace BuildMonitor.UI.Protection
{
    public class ProtectedInformation
    {
        public string DataHash { get; private set; }
        public string DataEntropy { get; private set; }

        public byte[] DataHashBytes
        {
            get { return Convert.FromBase64String(DataHash ?? string.Empty); }
            private set { DataHash = Convert.ToBase64String(value); }
        }

        public byte[] DataEntropyBytes
        {
            get { return Convert.FromBase64String(DataEntropy ?? string.Empty); }
            private set { DataEntropy = Convert.ToBase64String(value); }
        }

        public ProtectedInformation(byte[] dataHash, byte[] dataEntropy)
        {
            DataEntropyBytes = dataEntropy;
            DataHashBytes = dataHash;
        }

        public ProtectedInformation(string dataHash, string dataEntropy)
        {
            DataEntropy = dataEntropy;
            DataHash = dataHash;
        }
    }
}