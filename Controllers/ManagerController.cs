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
    public class ManagerController : ControllerBase
    {
        private readonly ManagerService _managerService;
        private readonly UserService _userService; 

        public ManagerController(ManagerService managerService, UserService userService)
        {
            _managerService = managerService;
            _userService = userService;
        }


    }
}