using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using PropTrac_backend.Models.Stripe;
using PropTrac_backend.Services;
using Stripe;
using static PropTrac_backend.Services.StripeService;

namespace server.Controllers
{

    [Route("account")]
    [ApiController]
    public class AccountApiController : Controller
    {

        private readonly StripeService _stripeService;

        public AccountApiController(StripeService stripeService)
        {
            _stripeService = stripeService;
        }

        [HttpPost]
        [Route("CreateNewAccount")]
        public IActionResult Create([FromBody] int UserId)
        {
            return _stripeService.Create(UserId);
        }

        [HttpPost]
        public IActionResult CreateLink([FromBody] AccountLinkPostBody accountLinkPostBody)
        {
            return _stripeService.CreateLink(accountLinkPostBody);
        }

        [HttpGet]
        [Route("GetAccount")]
        public IActionResult GetAccountInfo(string AccountID)
        {
            return _stripeService.GetAccountInfo(AccountID);
        }
    }
}