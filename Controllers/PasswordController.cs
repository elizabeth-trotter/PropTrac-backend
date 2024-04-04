using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PropTrac_backend.Services;

namespace PropTrac_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly PasswordService _passwordService;

        public PasswordController(PasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        [HttpGet("securityquestion/{questionId}")]
        public IActionResult GetSecurityQuestion(int questionId)
        {
            var question = _passwordService.GetSecurityQuestionById(questionId);
            if (question != null)
            {
                return Ok(question);
            }
            else
            {
                return NotFound();
            }
        }
    }
}