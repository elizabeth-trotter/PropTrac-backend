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

        // Property Revenue Overview

        // Monthly Profit

        // Property Stats
        [HttpGet]
        [Route("GetPropertyStatsByUserID/{userId}")]
        public IActionResult GetPropertyStatsByUserID(int userId)
        {
            // Check if the user exists
            var userExists = _userService.GetUserById(userId) != null;

            if (!userExists)
            {
                return NotFound("User does not exist");
            }

            var stats = _managerService.GetPropertyStats(userId);

            if (stats == null)
            {
                return Ok("Manager does not exist");
            }

            return Ok(stats);
        }

        // Maintenance Requests
        [HttpGet]
        [Route("GetMaintenanceStatsByUserID/{userId}")]
        public IActionResult GetMaintenanceStatsByUserID(int userId)
        {
            // Check if the user exists
            var userExists = _userService.GetUserById(userId) != null;

            if (!userExists)
            {
                return NotFound("User does not exist");
            }

            var stats = _managerService.GetMaintenanceStats(userId);

            if (stats == null)
            {
                return Ok("Manager does not exist");
            }

            return Ok(stats);
        }
    }
}