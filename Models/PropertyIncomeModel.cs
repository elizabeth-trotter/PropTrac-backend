using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models
{
    public class PropertyIncomeModel
    {
        public int ID { get; set; }
        public int Rent { get; set; }
        
        // Connection to PropertyInfoModel
        public int? PropertyInfoID { get; set; } // Foreign key
        public PropertyInfoModel? PropertyInfo { get; set; } // Navigation property
    }
}