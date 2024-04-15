using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropTrac_backend.Models.DTO;
using PropTrac_backend.Models.DTO.TenantDashboard;
using PropTrac_backend.Services;

namespace PropTrac_backend.Controllers
{
    // [Authorize] //*see note below
    [ApiController]
    [Route("[controller]")]
    public class TenantController : ControllerBase
    {
        private readonly TenantService _tenantService;
        private readonly UserService _userService; 

        public TenantController(TenantService tenantService, UserService userService)
        {
            _tenantService = tenantService;
            _userService = userService;
        }

        [HttpGet]
        [Route("GetTenantDashboardInfo/{userId}")]
        public IActionResult GetTenantDashboardInfo(int userId)
        {
            // Check if the user exists
            var userExists = _userService.GetUserById(userId) != null;

            if (!userExists)
            {
                return NotFound("User does not exist");
            }

            var tenants = _tenantService.GetTenantDashboardByUserId(userId);

            if (!tenants.Any())
            {
                return Unauthorized("Tenant does not exist");
            }

            return Ok(tenants);
        }

        [HttpPost]
        [Route("AddMaintenanceRequest")]
        public bool AddMaintenanceRequest(TenantMaintenanceDTO request){
            return _tenantService.AddMaintenanceRequest(request);
        }

        // [HttpGet]
        // [Route("GetTenantDashboardInfo")]
        // public IActionResult GetTenantDashboardInfo()
        // {
        //     // Extract claims from the user's identity
        //     var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        //     // var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        //     // var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        //     // var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        //     // Call the service method to get tenants based on the user's claims
        //     var tenants = _tenantService.GetTenantDashboardByUserId(int.Parse(userId));

        //     // Check if the user is authorized to access this endpoint
        //     if (tenants == null)
        //     {
        //         return Unauthorized();
        //     }

        //     return Ok(tenants);
        // }
    }
}

// Fetch request in client will require
// headers: {
//     "Authorization": `Bearer ${jwtToken}`
//   }