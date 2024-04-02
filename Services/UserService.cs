using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PropTrac_backend.Models;
using PropTrac_backend.Models.DTO;
using PropTrac_backend.Services.Context;

namespace PropTrac_backend.Services
{
    public class UserService : ControllerBase
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }
        public bool DoesUserExist(string usernameOrEmail)
        {
            //check if username or email exists
            //if 1 matches, then return the item
            //if no item matches, then return null

            return _context.UserInfo.SingleOrDefault(user => user.Username == usernameOrEmail || user.Email == usernameOrEmail) != null;
        }
        public bool AddUser(CreateAccountDTO UserToAdd)
        {
            bool result = false;

            //if user doesn't exist, add user
            if (!(DoesUserExist(UserToAdd.Username) || DoesUserExist(UserToAdd.Email)))
            {
                UserModel newUser = new UserModel();

                var hashPassword = HashPassword(UserToAdd.Password);

                newUser.ID = UserToAdd.ID;
                newUser.Username = UserToAdd.Username;
                newUser.Salt = hashPassword.Salt;
                newUser.Hash = hashPassword.Hash;
                newUser.Email = UserToAdd.Email;

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

        public IActionResult Login(LoginDTO User)
        {
            IActionResult Result = Unauthorized();

            //check if user exists
            if (DoesUserExist(User.UsernameOrEmail))
            {
                //if true, continue with authentication
                //if true, store our user object

                UserModel foundUser = GetUserByUsernameOrEmail(User.UsernameOrEmail);

                //check if password is correct
                if (VerifyUsersPassword(User.Password, foundUser.Hash, foundUser.Salt))
                {
                    //anyone with this code can access the login
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));

                    //sign in credentials
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    //generate new token and log user out after 30 min
                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>(), // Claims can be added here if needed
                        expires: DateTime.Now.AddMinutes(30), // Set token expiration time (e.g., 30 minutes)
                        signingCredentials: signinCredentials // Set signing credentials
                    );

                    // Generate JWT token as a string
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                    //return JWT token through http response with status code 200
                    Result = Ok(new { Token = tokenString });
                }

                //Token:
                // asdsafdafdafdf. = header
                // cmsjdjadds. = Payload: contains claims such as expiration time
                // iweoiwidjin. = signature encrypts and combines header and payload using secret key
            }

            return Result;
        }

        public UserModel GetUserByUsernameOrEmail(string usernameOrEmail)
        {
            // Query the database to find the user by username or email
            return _context.UserInfo.FirstOrDefault(user => user.Username == usernameOrEmail || user.Email == usernameOrEmail);
        }

        public UserModel GetUserByUsername(string username)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Username == username);
        }

        public bool UpdateUser(UserModel userToUpdate)
        {
            _context.Update<UserModel>(userToUpdate);
            return _context.SaveChanges() != 0;
        }

        public bool UpdateUsername(int id, string username)
        {
            //sending over just the id and username
            //we have to get the object to be updated

            UserModel foundUser = GetUserById(id);

            bool result = false;

            if (foundUser != null)
            {
                // a user was found
                // update foundUser object
                foundUser.Username = username;
                _context.Update<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        public UserModel GetUserById(int id)
        {
            return _context.UserInfo.SingleOrDefault(user => user.ID == id);
        }

        public bool DeleteUser(string userToDelete)
        {
            //we are only sending over the username
            //if username found, delete user

            UserModel foundUser = GetUserByUsername(userToDelete);

            bool result = false;

            if (foundUser != null)
            {
                //user was found

                _context.Remove<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }

            return result;
        }
    }
}