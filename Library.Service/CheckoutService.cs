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
<<<<<<< HEAD

=======
>>>>>>> 5beee2520e0e6fc8b3d9f07931753185678dddb3
        public bool CreateCheckout(CheckoutCreate model)
        {
            var entity =
                new Checkout()
                {
                    AdminId = _userId,

                    BookId = model.BookId,

                    LibraryCardId = model.LibraryCardId,

                    Quantity = model.Quantity,

<<<<<<< HEAD
                    DateOfCheckout = new DateTime(model.Year, model.Month, model.Day)
=======
                    DateOfCheckout = System.DateTime.UtcNow
>>>>>>> 5beee2520e0e6fc8b3d9f07931753185678dddb3
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Checkouts.Add(entity);
                return ctx.SaveChanges() == 1;

            }
        }
<<<<<<< HEAD
        public IEnumerable<CheckoutListItem> GetCheckouts(int checkoutId)
=======
        public IEnumerable<CheckoutListItem> GetCheckouts()
>>>>>>> 5beee2520e0e6fc8b3d9f07931753185678dddb3
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Checkouts
<<<<<<< HEAD
                    .Where(e => e.CheckoutID == checkoutId)
=======
                    .Where(e => e.AdminId == _userId)
>>>>>>> 5beee2520e0e6fc8b3d9f07931753185678dddb3
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
        public bool UpdateCheckout(CheckoutEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Checkouts
                    .Single(e => e.CheckoutID == model.CheckoutID && e.AdminId == _userId);

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

                    FullName = entity.FullName,

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
                    .Single(e => e.CheckoutID == checkoutId && e.AdminId == _userId);
                ctx.Checkouts.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
