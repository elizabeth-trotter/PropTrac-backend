using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PropTrac_backend.Models;
using PropTrac_backend.Models.DTO.Properties;
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

        // Six Month Profit Or Loss
        [HttpGet]
        [Route("GetPastSixMonthsProfitOrLoss/{userId}/{month}/{year}")]
        public IActionResult GetPastSixMonthsProfitOrLoss(int userId, int month, int year)
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

            var profitOrLossStatement = _managerService.GetPastSixMonthsProfitOrLoss(userId, month, year);
            return Ok(profitOrLossStatement);
        }

        // Projected
        [HttpGet]
        [Route("GetProjectedProfitOrLoss/{userId}/{month}/{year}")]
        public IActionResult GetProjectedProfitOrLoss(int userId, int month, int year)
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

            var profitOrLossStatement = _managerService.GetProjectedProfitOrLoss(userId, month, year);
            return Ok(profitOrLossStatement);
        }

        // Monthly Profit Or Loss
        [HttpGet]
        [Route("GetMonthlyProfitOrLoss/{userId}/{month}/{year}")]
        public IActionResult GetMonthlyProfitOrLoss(int userId, int month, int year)
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

        [HttpGet]
        [Route("GetAllProperties/{userId}")]
        public IActionResult GetAllProperties(int userId)
        {
            // Check if the user exists
            var userExists = _userService.GetUserById(userId) != null;

            if (!userExists)
            {
                return NotFound("User does not exist");
            }

            var manager = _managerService.GetManagerByUserId(userId);

            if (manager == null)
            {
                return Unauthorized("User is not an authorized Manager");
            }

            var properties = _managerService.GetAllProperties(userId);
            return Ok(properties);
        }

        [HttpPost]
        [Route("AddProperty")]
        public IActionResult AddProperty(AddPropertyDTO addPropertyDTO)
        {
            var userExists = _userService.GetUserById(addPropertyDTO.UserID) != null;

            if (!userExists)
            {
                return NotFound("User does not exist");
            }

            var manager = _managerService.GetManagerByUserId(addPropertyDTO.UserID);

            if (manager == null)
            {
                return Unauthorized("User is not an authorized Manager");
            }

            var result = _managerService.AddPropertyByUserID(addPropertyDTO);
            return Ok(result);
        }

        [HttpPut]
        [Route("EditProperty")]
        public bool EditProperty(EditPropertyDTO editPropertyDTO)
        {
            return _managerService.EditPropertyByID(editPropertyDTO);
        }

        [HttpDelete]
        [Route("DeleteProperty")]
        public bool DeleteProperty(DeletePropertyDTO deletePropertyDTO)
        {
            return _managerService.DeletePropertyById(deletePropertyDTO.PropertyID);
        }

        [HttpDelete]
        [Route("DeleteRoom")]
        public bool DeleteRoom(DeleteRoomDTO deleteRoomDTO)
        {
            return _managerService.DeleteRoomById(deleteRoomDTO.PropertyID, deleteRoomDTO.RoomID);
        }
    }
}