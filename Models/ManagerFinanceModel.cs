using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models
{
    public class ManagerFinanceModel
    {
        public int ID { get; set; }
        public int MonthlyRentRecieved { get; set; }

        // Connection to ManagerModel
        public int? ManagerID { get; set; } // Foreign key
        public ManagerModel? Manager { get; set; } // Navigation property
    }
}