using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models.DTO
{
    public class RequestResetPasswordDTO
    {
        public string UsernameOrEmail { get; set; }
    }
}