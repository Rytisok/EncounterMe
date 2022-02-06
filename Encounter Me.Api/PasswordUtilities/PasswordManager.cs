using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Encounter_Me
{
    public static class PasswordManager
    {
        // Hash the password using PBKDF2 algorithm.
        public static HashAndSalt EncryptPassword(string password, byte[] storedSalt = null)
        {
            var salt = new byte[128 / 8];
            if(storedSalt != null)
            {
                salt = storedSalt;
            }
            else
            {
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    rngCsp.GetNonZeroBytes(salt);
                }
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

            return new HashAndSalt { Hash = hashed, Salt = salt };
        }

        public static bool IsPasswordCorrect(string enteredPassword, byte[] storedSalt, string storedPassword)
        {
            return EncryptPassword(enteredPassword, storedSalt).Hash == storedPassword;
        }
    }
}
