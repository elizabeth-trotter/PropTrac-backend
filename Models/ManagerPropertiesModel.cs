using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models
{
    public class ManagerPropertiesModel
    {
        public int ID { get; set; }
        
        // Connection to ManagerModel
        public int ManagerID { get; set; } // Foreign key
        public ManagerModel Manager { get; set; } // Navigation property

        // Connection to PropertyInfoModel
        public int PropertyInfoID { get; set; } // Foreign key
        public PropertyInfoModel PropertyInfo { get; set; } // Navigation property
    }
}