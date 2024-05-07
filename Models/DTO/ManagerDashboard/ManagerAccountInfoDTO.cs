using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models.DTO.ManagerDashboard
{
    public class ManagerAccountInfoDTO
    {
        public int ID { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
        public string? Phone { get; set; } // Values can be null because these will not be filled during account creation**
        public string? Role { get; set; }
        public string? Location { get; set; }
        public string? Language { get; set; }
    }
}