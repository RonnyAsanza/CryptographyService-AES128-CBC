namespace Encrypted_and_Decrypted
{
    public interface ICryptographyService
    {
        string Encrypt(object plainText);
        string Decrypt(string cipherText);
    }
}
