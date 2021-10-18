﻿using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Encounter_Me
{
    public static class PaswordManager
    {

        // Generate a 128-bit salt using a secure PRNG
        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8]; 
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        // Hash the password using PBKDF2 algorithm
        public static string EncryptPassword(string password, byte[] salt)
        {
            string encryptedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2( 
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            ));
            return encryptedPassword;
        }

        public static bool IsPasswordCorrect(string enteredPassword, byte[] salt, string storedPassword)
        {
            if(EncryptPassword(enteredPassword, salt) == storedPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // IN context:
        //{
        // ...
        //  var salt = PasswordManager.GenerateSalt();
        //  var hashedPassword = PaswordManager.EncryptPassword(password, salt);
        //  user.Password = hashedPassword;
        //  user.StoredSalt = salt;
        //}

    }
}