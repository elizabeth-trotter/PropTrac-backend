using System;
using System.Collections.Generic;
using System.Linq;
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
                result = true;
            }

            return result;
        }
    }
}