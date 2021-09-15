using Library.Data;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class LibraryCardService
    {
        private readonly Guid _userId;

        public LibraryCardService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateLibraryCard(LibraryCardCreate model)
        {
            var entity =
                new LibraryCard()
                {
                    AdminId = _userId,
                    LibraryCardId = model.LibraryCardId,
                    FullName = model.FullName,
                    Address = model.Address,
                    BookId = model.BookId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.LibraryCards.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<LibraryCardListItem> GetLibraryCards()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .LibraryCards
                        .Where(c => c.AdminId == _userId)
                        .Select(
                            c =>
                                new LibraryCardListItem
                                {
                                    LibraryCardId = c.LibraryCardId,
                                    FullName = c.FullName,
                                    Address = c.Address,
                                    BookId = ctx.Books.Where(e => e.BookId == c.BookId).Select(e => new BookList

                                    {
                                        BookId = e.BookId,
                                        Title = e.Title,
                                        ISBN = e.ISBN,
                                        AuthorName = e.AuthorName,
                                        PublishedDate = e.PublishedDate
                                    })
                                }
                                );
                return query.ToArray();
            }
        }

        public LibraryCardDetail GetLibraryCardById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .LibraryCards
                        .Single(c => c.LibraryCardId == id && c.AdminId == _userId);
                return
                    new LibraryCardDetail
                    {
                        LibraryCardId = entity.LibraryCardId,
                        FullName = entity.FullName
                    };
            }
        }


        public bool UpdateLibraryCard(LibraryCardEdit card)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .LibraryCards
                        .Single(c => c.LibraryCardId == card.LibraryCardId && c.AdminId == _userId);


                entity.LibraryCardId = card.LibraryCardId;
                entity.FullName = card.FullName;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteLibraryCard(int cardId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .LibraryCards
                        .Single(c => c.LibraryCardId == cardId && c.AdminId == _userId);

                ctx.LibraryCards.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
