using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using PropTrac_backend.Models.DTO;

namespace PropTrac_backend.Utilities
{
    public static class PasswordUtility
    {
        public static PasswordDTO HashSeedDataPassword(string password)
        {
            PasswordDTO newHashPassword = new PasswordDTO();

            //create a byte array using 64 bytes of salt.
            byte[] SaltByte = new byte[64];

            //this is our randomizer
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

            //take SaltByte and make sure it contains no zero's. Making it more secure.
            provider.GetNonZeroBytes(SaltByte);

            //converting our salt into a string
            string salt = Convert.ToBase64String(SaltByte);

            //encode our password with salt into our hash. 10000 times, where 10000 is a common starting point
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltByte, 10000);

            //convert the resulting byte array as a string of 256 bytes
            string hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            //we can save our hash and salt into our passwordDTO
            newHashPassword.Salt = salt;
            newHashPassword.Hash = hash;

            return newHashPassword;
        }
    }
}