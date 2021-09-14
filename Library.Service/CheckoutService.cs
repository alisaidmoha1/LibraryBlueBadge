using Library.Api.Data;
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
        public bool CreateCheckout(CheckoutCreate model)
        {
            var entity =
                new Checkout()
                {
                    CheckoutID = model.CheckoutID,

                    BookId = model.CheckoutID,

                    LibraryCardId = model.LibraryCardId,

                    FullName = model.FullName,

                    DateOfCheckout = model.DateOfCheckout
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Checkouts.Add(entity);
                return ctx.SaveChanges() == 1;

            }
        }
        public IEnumerable<CheckoutListItem> GetCheckouts(int libraryCardId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Checkouts
                    .Where(e => e.LibraryCardId == libraryCardId)
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
    }
}
