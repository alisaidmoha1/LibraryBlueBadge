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
<<<<<<< HEAD
            var checkoutId = Guid.Parse(User.Identity.GetUserId());
            var checkoutService = new CheckoutService(checkoutId);
=======
            var userId = Guid.Parse(User.Identity.GetUserId());
            var checkoutService = new CheckoutService(userId);
>>>>>>> 5beee2520e0e6fc8b3d9f07931753185678dddb3
            return checkoutService;
            ;
        }
<<<<<<< HEAD
        
        public IHttpActionResult Get(int checkoutId)
        {
            CheckoutService checkoutService = CreateCheckoutService();
            var checkouts = checkoutService.GetCheckouts(checkoutId);
            return Ok(checkouts);
=======
        public IHttpActionResult Get()
        {
            CheckoutService checkoutService = CreateCheckoutService();
            var checkout = checkoutService.GetCheckouts();

            return Ok(checkout);
>>>>>>> 5beee2520e0e6fc8b3d9f07931753185678dddb3
        }
        
        public IHttpActionResult Post(CheckoutCreate checkout)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ctx = new ApplicationDbContext();
            Book books = ctx.Books.Find(checkout.BookId);
            

            if (checkout.Quantity <= 0)
                return BadRequest("You have to take at least one book to checkout");

            if (books == null)
                return BadRequest("Invalid Book ID");

            if (books.Quantity < 0)
                return BadRequest("This book is out of stock right now");

            if (books.Quantity < checkout.Quantity)
                return BadRequest("Not enough books are in the library");

            books.Quantity -= checkout.Quantity;

            ctx.SaveChanges();

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

