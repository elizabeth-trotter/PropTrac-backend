using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models
{
    public class PropertyExpenseModel
    {
        public int ID { get; set; }
        public int Mortage { get; set; }
        public int MaintenceCosts { get; set; }

        // Connection to PropertyInfoModel
        public int? PropertyInfoID { get; set; } // Foreign key
        public PropertyInfoModel? PropertyInfo { get; set; } // Navigation property
    }
}