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

        // Monthly Profit Or Loss
        [HttpGet]
        [Route("GetMontlyProfitOrLossByUserID/{userId}/{month}/{year}")]
        public IActionResult GetMontlyProfitOrLossByUserID(int userId, int month, int year)
        {
            // Check if the user exists
            var userExists = _userService.GetUserById(userId) != null;

            if (!userExists)
            {
                return NotFound("User does not exist");
            }

            // Check if the manager exists
            var manager = _managerService.GetManagerByUserId(userId);

            if (manager == null)
            {
                return Unauthorized("User is not an authorized Manager");
            }

            var profitOrLossStatement = _managerService.GetMonthlyProfitOrLoss(userId, month, year);
            return Ok(profitOrLossStatement);
        }

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
                return Unauthorized("User is not an authorized Manager");
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
                return Unauthorized("User is not an authorized Manager");
            }

            return Ok(stats);
        }
    }
}