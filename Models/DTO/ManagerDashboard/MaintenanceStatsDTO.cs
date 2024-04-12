using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models.DTO.ManagerDashboard
{
    public class MaintenanceStatsDTO
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public DateTime DateRequested { get; set; }

        public int PropertyInfoID { get; set; }
    }
}