using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models
{
    public class TenantPaymentInfoModel
    {
        public int ID { get; set; } // Primary Key
        public int Balance { get; set; }
        public DateTime DueDate { get; set; }
        public bool PaymentRecieved { get; set; }
        public int DaysRemaining { get; set; }

        // Connection to TenantModel
        public int TenantID { get; set; } // Foreign key
        public TenantModel Tenant { get; set; } // Navigation property

        public TenantPaymentInfoModel()
        {
            
        }
    }
}