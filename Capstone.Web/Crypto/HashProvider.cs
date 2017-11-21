using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Capstone.Web.Crypto
{
    public class HashProvider
    {
        private const int WorkFactor = 10000;

        public string SaltValue { get; private set; }


        public string HashPasswordWithMD5(string password, int saltSize, int workFactor)
        {
            //Create the hashing provider
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltSize, workFactor);

            byte[] hash = rfc2898DeriveBytes.GetBytes(20);      //gets the hashed password
            byte[] salt = rfc2898DeriveBytes.Salt;              //gets the random salt

            return Convert.ToBase64String(salt) + "|" + Convert.ToBase64String(hash);
        }

        public bool VerifyPasswordMatch(string existingHashedPassword, string plainTextPassword, string salt)
        {
            byte[] saltArray = Convert.FromBase64String(salt);      //gets us the byte[] array representation

            //Create the hashing provider
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(plainTextPassword, saltArray, WorkFactor);

            //Get the hashed password
            byte[] hash = rfc.GetBytes(20);

            //Compare the hashed password values
            string newHashedPassword = Convert.ToBase64String(hash);

            return (existingHashedPassword == newHashedPassword);
        }
    }
}