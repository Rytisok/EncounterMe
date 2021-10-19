using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Encounter_Me
{
    public static class PasswordManager
    {

        
        // Hash the password using SHA256 algorithm.
        public static HashAndSalt EncryptPassword(string password, byte[] storedSalt = null)
        {
            var salt = new byte[128 / 8];
            if(storedSalt != null)
            {
                salt = storedSalt;
                // Gives posibility to use this method for authentification of entered password.
            }
            else
            {
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(salt);
                // Ensures that salt is always generated.
            }

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            SHA256Managed sha256hashstring = new SHA256Managed();
            var encryptedPassword = Convert.ToBase64String(sha256hashstring.ComputeHash(bytes));


            return new HashAndSalt { Hash = encryptedPassword, Salt = salt };
        }

        public static bool IsPasswordCorrect(string enteredPassword, byte[] storedSalt, string storedPassword)
        {
            return EncryptPassword(enteredPassword, storedSalt).Hash == storedPassword;
        }

        // IN context:
        //{
        //   var hashSalt = encryptPassword(enteredPassword);
        //   user.Password = hashSalt.Hash;
        //   user.StoredSalt = hashSalt.Salt;
        //}

    }
}
