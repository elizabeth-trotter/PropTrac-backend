using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models.Stripe
{
    public class StripeAccountModel
    {
        public int ID { get; set; } // Primary Key
        public string Account { get; set; }
    }
}