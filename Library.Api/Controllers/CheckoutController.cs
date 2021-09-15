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
            var checkoutId = Guid.Parse(User.Identity.GetUserId());
            var checkoutService = new CheckoutService(checkoutId);
            return checkoutService;
            ;
        }
        
        public IHttpActionResult Get(int checkoutId)
        {
            CheckoutService checkoutService = CreateCheckoutService();
            var checkouts = checkoutService.GetCheckouts(checkoutId);
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
        
        public IHttpActionResult Put(CheckoutEdit checkout)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCheckoutService();
            if (!service.UpdateCheckout(checkout))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int checkoutId)
        {
            var service = CreateCheckoutService();
            if (!service.DeleteCheckout(checkoutId))
                return InternalServerError();

            return Ok();
        }
    }
}

