using Library.Data;
using Library.Model;
using Library.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library.Api.Controllers
{
    [Authorize]
    public class CheckoutController : ApiController
    {
        private CheckoutService CreateCheckoutService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var checkoutService = new CheckoutService(userId);
            return checkoutService;
        }
        public IHttpActionResult Get()
        {
            CheckoutService checkoutService = CreateCheckoutService();
            var checkout = checkoutService.GetCheckouts();

            return Ok(checkout);
        }
        public IHttpActionResult Post(CheckoutCreate checkout)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCheckoutService();

            if (!service.CreateCheckout(checkout))
                return InternalServerError();

            return Ok();
        }
    }
}
