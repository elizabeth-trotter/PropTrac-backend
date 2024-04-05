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

        [HttpGet("SecurityQuestion/{questionId}")]
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

        [HttpPost("RequestReset")]
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

        // If you don't want the username to be visible in the URL for security or privacy reasons, you can still use a GET request with a request body. However, traditional HTTP standards do not support sending a body with GET requests.

        // One common workaround is to use a POST request instead of a GET request. Although POST requests are typically used for sending data to the server in the request body, it's not strictly forbidden to use a POST request to retrieve data, especially if you need to keep the data confidential.

        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var success = _passwordService.ResetPassword(resetPasswordDTO);
            if (success)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}