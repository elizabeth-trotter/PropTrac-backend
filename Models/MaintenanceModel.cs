using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models
{
    public class MaintenanceModel
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public DateTime DateRequested { get; set; }
        public string? ContractorName { get; set; }
        public string? ContractorEmail { get; set; }
        public string? ContractorPhone { get; set; }

        // Connection to PropertyMaintenanceModel
        public PropertyMaintenanceModel? PropertyMaintenance { get; set; }  // Navigation property

        public MaintenanceModel()
        {
            DateRequested = DateTime.Now;
        }
    }
}