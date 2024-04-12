using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models
{
    public class PropertyMaintenanceModel
    {
        [Key]
        public int ID { get; set; }
        
        // Connection to PropertyInfoModel
        [ForeignKey("PropertyInfo")]
        public int? PropertyInfoID { get; set; } // Foreign key
        public PropertyInfoModel? PropertyInfo { get; set; } // Navigation property
        
        // Connection to MaintenanceModel
        [ForeignKey("Maintenance")]
        public int? MaintenanceID { get; set; } // Foreign key
        public MaintenanceModel? Maintenance { get; set; } // Navigation property
    }
}