using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PropTrac_backend.Models;
using PropTrac_backend.Models.DTO;
using PropTrac_backend.Services.Context;

namespace PropTrac_backend.Services
{
    public class PasswordService
    {
        private readonly DataContext _context;
        private readonly UserService _userService; // Add reference to UserService

        public PasswordService(DataContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public string GetSecurityQuestionById(int questionId)
        {
            var securityQuestion = _context.SecurityQuestion.FirstOrDefault(q => q.ID == questionId);
            return securityQuestion != null ? securityQuestion.Question : null;
        }

        public string RequestResetPassword(RequestResetPasswordDTO requestResetPasswordDTO)
        {
            var user = _userService.GetUserByUsernameOrEmail(requestResetPasswordDTO.UsernameOrEmail);
            if (user != null)
            {
                var securityQuestion = GetSecurityQuestionById(user.SecurityQuestionID);
                return securityQuestion;
            }
            else
            {
                return null;
            }
        }

        // No need to duplicate GetUserByUsernameOrEmail method

        public bool ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            bool result = false;

            UserModel foundUser = _userService.GetUserByUsernameOrEmail(resetPasswordDTO.UsernameOrEmail);
            if (foundUser != null)
            {
                if (foundUser.SecurityAnswer == resetPasswordDTO.SecurityAnswer)
                {
                    // reset password
                    var hashPassword = _userService.HashPassword(resetPasswordDTO.NewPassword);
                    foundUser.Salt = hashPassword.Salt;
                    foundUser.Hash = hashPassword.Hash;
                    
                    _context.Update<UserModel>(foundUser);
                    result = _context.SaveChanges() != 0;
                }
            }

            return result;

        }

        // No need to duplicate HashPassword method
    }
}