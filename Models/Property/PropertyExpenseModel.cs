using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models.Property
{
    public class PropertyExpenseModel
    {
        public int ID { get; set; }
        public int PropertyInfoID { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public bool IsRecurring { get; set; } // Flag for recurring expenses
        public bool IsFixedAmount { get; set; } // Flag for fixed or averaged amount

        // Navigation property to PropertyInfoModel
        public PropertyInfoModel PropertyInfo { get; set; }
    }
}