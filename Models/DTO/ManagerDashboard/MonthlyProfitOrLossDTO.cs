using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models.DTO.ManagerDashboard
{
    public class MonthlyProfitOrLossDTO
    {
        public int Month { get; set; }
        public int ExpenseTotal { get; set; }
        public int RevenueTotal { get; set; }
        public int ProfitOrLossAmount { get; set; }
    }
}