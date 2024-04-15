using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models.Property
{
    public class MonthlyPropertyFinanceModel
    {
        public int ID { get; set; }
        public int PropertyInfoID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int ExpenseAmount { get; set; }
        public int IncomeAmount { get; set; }

        // Navigation property to PropertyInfoModel
        public PropertyInfoModel PropertyInfo { get; set; }
    }
}