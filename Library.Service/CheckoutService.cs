using Library.Data;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Service
{
    public class CheckoutService
    {
        private readonly Guid _userId;
        

        public CheckoutService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateCheckout(CheckoutCreate model)
        {
            var entity =
                new Checkout()
                {
                    AdminId = _userId,

                    BookId = model.BookId,                    

                    LibraryCardId = model.LibraryCardId,

                    Quantity = model.Quantity,

                    DateOfCheckout = System.DateTime.UtcNow

                };
            
            using (var ctx = new ApplicationDbContext())
            { 
                ctx.Checkouts.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }
                
        public IEnumerable<CheckoutListItem> GetCheckouts()
        {

            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Checkouts

                    .Where(e => e.AdminId == _userId).ToList();

                var queryResults = new List<CheckoutListItem>();

                foreach (var checkout in query)
                {
                    var checkoutReceipt = new CheckoutListItem
                    {
                        CheckoutID = checkout.CheckoutID,

                        BookId = checkout.BookId,

                        BookName = ctx.Books.Single(b => b.BookId == checkout.BookId).Title,

                        LibraryCardId = checkout.LibraryCardId,

                        FullName = ctx.LibraryCards.Single(l => l.LibraryCardId == checkout.LibraryCardId).FullName,

                        Quantity = checkout.Quantity,

                        DateOfCheckout = checkout.DateOfCheckout

                    };
                    queryResults.Add(checkoutReceipt);

                };
                                    
                return queryResults;
            }
        }
        public bool UpdateCheckout(CheckoutEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Checkouts
                    .Single(e => e.CheckoutID == model.CheckoutID);

                entity.CheckoutID = model.CheckoutID;

                entity.DateOfCheckout = DateTime.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public CheckoutDetail GetCheckoutById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Checkouts
                    .Single(e => e.CheckoutID == id && e.AdminId == _userId);

                return new CheckoutDetail
                {
                    CheckoutID = entity.CheckoutID,

                    BookId = entity.BookId,

                    LibraryCardId = entity.LibraryCardId,
                                       
                    DateOfCheckout = entity.DateOfCheckout
                };
            }
        }
        public bool DeleteCheckout(int checkoutId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Checkouts
                    .Single(e => e.CheckoutID == checkoutId);
                ctx.Checkouts.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public bool ReturnCheckout(int checkoutId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var checkout =
                    ctx
                    .Checkouts
                    .Single(e => e.CheckoutID == checkoutId);
                //when returning a book look at checkout and reduce quantity by 1
                
                checkout.Quantity--;
                
                //increase library quantity by one
                
                var book =
                    ctx
                    .Books
                    .Single(e => e.BookId == checkout.BookId);
                book.Quantity++;
                
                //if quantity = 0, delete checkout and increase library quantity by one
                
                var result = ctx.SaveChanges() == 1;

                if (checkout.Quantity == 0)
                {
                    DeleteCheckout(checkoutId);
                }
                return result;


            }
        }
    }
}
