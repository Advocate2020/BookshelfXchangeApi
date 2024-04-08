using System.Security.Cryptography;

namespace BookXChangeApi.Util
{
    public class HashUtil
    {
        public static string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt;
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt = new byte[128 / 8]);
            }

            // Define the iteration count for the hash function
            int iterations = 10000;

            // Derive a hash value using the PBKDF2 algorithm
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);

            // Get the hash bytes
            byte[] hashBytes = pbkdf2.GetBytes(256 / 8);

            // Combine the salt and hash bytes into a single byte array
            byte[] hashWithSaltBytes = new byte[salt.Length + hashBytes.Length];
            Array.Copy(salt, 0, hashWithSaltBytes, 0, salt.Length);
            Array.Copy(hashBytes, 0, hashWithSaltBytes, salt.Length, hashBytes.Length);

            // Convert the byte array to a base64-encoded string
            string hashWithSaltString = Convert.ToBase64String(hashWithSaltBytes);

            return hashWithSaltString;
        }


        public static bool VerifyPassword(string hashedPassword, string password)
        {
            // Convert the base64-encoded string back to a byte array
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashedPassword);

            // Extract the salt from the byte array
            int saltSize = hashWithSaltBytes.Length - (256 / 8); // Compute salt size based on hash size
            byte[] salt = new byte[saltSize];
            Array.Copy(hashWithSaltBytes, 0, salt, 0, saltSize);

            // Derive a hash value using the same salt and iteration count
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hashBytes = pbkdf2.GetBytes(256 / 8);

            // Compare the computed hash with the hash stored in the hashWithSaltBytes
            for (int i = 0; i < hashBytes.Length; i++)
            {
                if (hashWithSaltBytes[i + saltSize] != hashBytes[i])
                {
                    return false; // Hash mismatch
                }
            }
            return true; // Hash match
        }

    }
}
