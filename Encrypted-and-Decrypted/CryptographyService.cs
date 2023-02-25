using System.Security.Cryptography;

namespace Encrypted_and_Decrypted
{
    public class CryptographyService : ICryptographyService
    {
        private readonly int _blockSize;

        private readonly int _keySize;

        private readonly CipherMode _mode;

        private readonly PaddingMode _padding;

        private readonly byte[] _key;

        private readonly byte[] _IV;

        public CryptographyService()
        {
            _blockSize = 128;
            _keySize = 128;
            _mode = CipherMode.CBC;
            _padding = PaddingMode.PKCS7;
            _key = new byte[16]
            {
                150, 160, 32, 165, 214, 53, 200, 157, 177, 67,
                141, 206, 161, 159, 97, 253
            };
            _IV = new byte[16]
            {
                253, 160, 32, 165, 214, 253, 200, 157, 177, 67,
                165, 206, 161, 159, 97, 253
            };
        }

        public string Encrypt(object plainText)
        {
            byte[] inArray;
            using (Aes aes = Aes.Create())
            {
                aes.BlockSize = _blockSize;
                aes.KeySize = _keySize;
                aes.Mode = _mode;
                aes.Padding = _padding;
                aes.Key = _key;
                aes.IV = _IV;
                ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);
                using MemoryStream memoryStream = new MemoryStream();
                using CryptoStream stream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new StreamWriter(stream))
                {
                    streamWriter.Write(plainText);
                }

                inArray = memoryStream.ToArray();
            }

            return Convert.ToBase64String(inArray);
        }

        public string Decrypt(string cipherText)
        {
            byte[] buffer = Convert.FromBase64String(cipherText);
            string result = string.Empty;
            using (Aes aes = Aes.Create())
            {
                aes.BlockSize = _blockSize;
                aes.KeySize = _keySize;
                aes.Mode = _mode;
                aes.Padding = _padding;
                aes.Key = _key;
                aes.IV = _IV;
                ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);
                using MemoryStream stream = new MemoryStream(buffer);
                using CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
                using StreamReader streamReader = new StreamReader(stream2);
                result = streamReader.ReadToEnd();
            }

            return result;
        }
    }
}
