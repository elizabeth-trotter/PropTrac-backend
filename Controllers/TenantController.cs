using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropTrac_backend.Services;

namespace PropTrac_backend.Controllers
{
    [Authorize] //*see note below
    [ApiController]
    [Route("[controller]")]
    public class TenantController : ControllerBase
    {
        private readonly TenantService _tenantService;

        public TenantController(TenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet]
        [Route("GetTenantDashboardInfo")]
        public IActionResult GetTenantDashboardInfo()
        {
            // Extract claims from the user's identity
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            // var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            // var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            // var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            // Call the service method to get tenants based on the user's claims
            var tenants = _tenantService.GetTenantDashboardByUserId(int.Parse(userId));

            // Check if the user is authorized to access this endpoint
            if (tenants == null)
            {
                return Unauthorized();
            }

            return Ok(tenants);
        }
    }
}

// Fetch request in client will require
// headers: {
//     "Authorization": `Bearer ${jwtToken}`
//   }