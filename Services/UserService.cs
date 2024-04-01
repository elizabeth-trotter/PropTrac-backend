using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using PropTrac_backend.Models;
using PropTrac_backend.Models.DTO;
using PropTrac_backend.Services.Context;

namespace PropTrac_backend.Services
{
    public class UserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }
        public bool DoesUserExist(string Username)
        {
            //check if username exists
            //if 1 matches, then return the item
            //if no item matches, then return null

            return _context.UserInfo.SingleOrDefault(user => user.Username == Username) != null;
        }
        public bool AddUser(CreateAccountDTO UserToAdd)
        {
            bool result = false;

            //if user doesn't exist, add user
            if (!DoesUserExist(UserToAdd.Username))
            {
                UserModel newUser = new UserModel();

                var hashPassword = HashPassword(UserToAdd.Password);

                newUser.ID = UserToAdd.ID;
                newUser.Username = UserToAdd.Username;
                newUser.Salt = hashPassword.Salt;
                newUser.Hash = hashPassword.Hash;

                //adds newUser to the database
                _context.Add(newUser);

                //save into database, return of number of entries written into database
                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        // HashedPassword = H(Salt + Password)

        public PasswordDTO HashPassword(string password)
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

        // verify user's password
        public bool VerifyUsersPassword(string? password, string? storedHash, string? storedSalt)
        {

            //encode salt back into the original byte array
            byte[] SaltBytes = Convert.FromBase64String(storedSalt);

            //repeat process of taking password entered and hashing it again
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltBytes, 10000);

            //Rfc289 object, retrieve the 256 bytes of hash, convert those into a string, assign the result to the newHash
            string newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            return newHash == storedHash;
        }
    }
}