using Library.Data;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    CheckoutID = model.CheckoutID,

                    BookId = model.CheckoutID,

                    LibraryCardId = model.LibraryCardId,

                    FullName = model.FullName,

                    DateOfCheckout = new DateTime(model.Year, model.Month, model.Day)
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Checkouts.Add(entity);
                return ctx.SaveChanges() == 1;

            }
        }
        public IEnumerable<CheckoutListItem> GetCheckouts(int checkoutId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Checkouts
                    .Where(e => e.CheckoutID == checkoutId)
                    .Select(
                        e =>
                        new CheckoutListItem
                        {
                            CheckoutID = e.CheckoutID,

                            BookId = e.BookId,

                            LibraryCardId = e.LibraryCardId,

                            FullName = e.FullName,

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
