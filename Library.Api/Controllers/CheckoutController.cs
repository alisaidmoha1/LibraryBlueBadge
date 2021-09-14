using Library.Model;
using Library.Service;
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
            var checkoutService = new CheckoutService();
            return checkoutService;
        }
        public IHttpActionResult Get(int libraryId)
        {
            CheckoutService checkoutService = CreateCheckoutService();
            var checkouts = checkoutService.GetCheckouts(libraryId);
            return Ok(checkouts);
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
