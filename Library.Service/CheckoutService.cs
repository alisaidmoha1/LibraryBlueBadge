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
                    .Where(e => e.AdminId == _userId)
                    .Select(
                        e =>
                        new CheckoutListItem
                        {
                            CheckoutID = e.CheckoutID,

                            BookId = e.BookId,

                            LibraryCardId = e.LibraryCardId,

                            Quantity = e.Quantity,

                            DateOfCheckout = e.DateOfCheckout

                        }
                        );
                return query.ToArray();
            }
        }
    }
}
