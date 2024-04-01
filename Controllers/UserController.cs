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
    public class UserController : ControllerBase
    {
        private readonly UserService _data;
        public UserController(UserService data)
        {
            _data = data;
        }

        // Login endpoint

        // AddUser endpoint
            // if user already exists
            // if user does not exist, create new account
            // else return false

        public bool AddUser(CreateAccountDTO UserToAdd)
        {
            return _data.AddUser(UserToAdd);
        }
    }
}