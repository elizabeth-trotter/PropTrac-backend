using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PropTrac_backend.Services.Context;

namespace PropTrac_backend.Services
{
    public class PasswordService
    {
        private readonly DataContext _context;
        public PasswordService(DataContext context)
        {
            _context = context;
        }

        public string GetSecurityQuestionById(int questionId)
        {
            var securityQuestion = _context.SecurityQuestion.FirstOrDefault(q => q.ID == questionId);
            return securityQuestion != null ? securityQuestion.Question : null;
        }
    }
}