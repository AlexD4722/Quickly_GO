namespace QuicklyGo.Unit
{
    public class CreateGenerateUniqueKey
    {
        public static string GenerateUniqueKey(int lengKey = 21)
        {
            int keySize = lengKey; // Define the size of the key
            char[] availableChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] randomBytes = new byte[keySize];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes); // Fill the array with random bytes
            }
            char[] characters = new char[keySize];
            for (int i = 0; i < keySize; i++)
            {
                characters[i] = availableChars[randomBytes[i] % availableChars.Length];
            }
            return new string(characters);
        }
    }
}
