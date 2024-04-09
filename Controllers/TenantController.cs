using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly TenantService _data;
        public TenantController(TenantService data)
        {
            _data = data;
        }
    }
}

// Fetch request in client will require
// headers: {
//     "Authorization": `Bearer ${jwtToken}`
//   }