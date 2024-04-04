using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PropTrac_backend.Models.DTO;
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

        [HttpGet("Securityquestion/{questionId}")]
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

        [HttpPost("Requestresetpassword")]
        public IActionResult RequestResetPassword(RequestResetPasswordDTO requestResetPasswordDTO)
        {
            var question = _passwordService.RequestResetPassword(requestResetPasswordDTO);
            if (question != null)
            {
                return Ok(question);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("Resetpassword")]
        public IActionResult ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var success = _passwordService.ResetPassword(resetPasswordDTO);
            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}