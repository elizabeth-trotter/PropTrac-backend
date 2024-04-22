using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models.DTO.TenantDashboard
{
    public class TenantMaintenanceDTO
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
        public string? Image { get; set; }
        public int UserID { get; set; }
    }
}