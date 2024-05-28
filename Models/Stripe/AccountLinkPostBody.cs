using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe;

namespace PropTrac_backend.Models.Stripe
{
    public class AccountLinkPostBody
    {
        public string Account { get; set; }
    }
}