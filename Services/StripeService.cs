using System;
using Stripe;
using Microsoft.AspNetCore.Mvc;
using PropTrac_backend.Services.Context;
using PropTrac_backend.Models.Stripe;

namespace PropTrac_backend.Services
{
    public class StripeService
    {
        private readonly DataContext _context;

        public StripeService(DataContext context)
        {
            _context = context;
        }

        public class Startup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddMvc().AddNewtonsoftJson();
            }
        }

        public IActionResult Create([FromBody] int UserID)
        {
            try
            {

                StripeConfiguration.ApiKey = "sk_test_51OuSqPRpUHAYLSKNlXu73Kg5wa1h9vHZQGWyZIGIB6JIExsJViF1rytzfmzdTMuKRyyT1As3Usuxa9Ti37k2VjdG009TzkcvgL";
                var service = new AccountService();

                var options = new AccountCreateOptions { Type = "express" };

                Account account = service.Create(options);

                StripeAccountModel stripeAccountModel = new()
                {
                    Account = account.Id,
                    ID = UserID,
                };

                _context.StripeAccount.Add(stripeAccountModel);

                return Json(new
                {
                    account = account.Id
                });
            }
            catch (Exception ex)
            {
                Console.Write("An error occurred when calling the Stripe API to create an account:  " + ex.Message);

                return StatusCode(500, new { error = ex.Message });
            }
        }


        public IActionResult CreateLink([FromBody] AccountLinkPostBody accountLinkPostBody)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51OuSqPRpUHAYLSKNlXu73Kg5wa1h9vHZQGWyZIGIB6JIExsJViF1rytzfmzdTMuKRyyT1As3Usuxa9Ti37k2VjdG009TzkcvgL";
                var connectedAccountId = accountLinkPostBody.Account;
                var service = new AccountLinkService();

                AccountLink accountLink = service.Create(
                    new AccountLinkCreateOptions
                    {
                        Account = connectedAccountId,
                        ReturnUrl = $"http://localhost/3000/",
                        RefreshUrl = $"http://localhost:3000/",
                        Type = "account_onboarding",
                    }
                );

                return Json(new { url = accountLink.Url });
            }
            catch (Exception ex)
            {
                Console.Write("An error occurred when calling the Stripe API to create an account link:  " + ex.Message);
                return StatusCode(500, new { error = ex.Message });
            }
        }

         public IActionResult GetAccountInfo(string AccountID)
        {
            try
            {
                 StripeConfiguration.ApiKey = "sk_test_51OuSqPRpUHAYLSKNlXu73Kg5wa1h9vHZQGWyZIGIB6JIExsJViF1rytzfmzdTMuKRyyT1As3Usuxa9Ti37k2VjdG009TzkcvgL";
             var service = new AccountService();

                return Json(new { account = service.Get(AccountID)});
            }
            catch(Exception ex) {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        private IActionResult Json(object value)
        {
            return new JsonResult(value);
        }

        private IActionResult StatusCode(int v, object value)
        {
            return new ObjectResult(value) { StatusCode = v };
        }
    }
}