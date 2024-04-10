using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models
{
    public class ManagerPropertiesModel
    {
        [Key]
        public int ID { get; set; }
        
        // Connection to ManagerModel
        [ForeignKey("Manager")]
        public int? ManagerID { get; set; } // Foreign key
        public ManagerModel? Manager { get; set; } // Navigation property

        // Connection to PropertyInfoModel
        [ForeignKey("PropertyInfo")]
        public int? PropertyInfoID { get; set; } // Foreign key
        public PropertyInfoModel? PropertyInfo { get; set; } // Navigation property
    }
}