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
                    LibraryCardId = model.LibraryCardId,
                    FullName = model.FullName,
                    Address = model.Address
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
                        .Where(c => c.LibraryCardsId == _userId)
                        .Select(
                            c =>
                                new LibraryCardListItem
                                {
                                    LibraryCardId = c.LibraryCardId,
                                    FullName = c.FullName,
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
                        .Single(c => c.LibraryCardId == id);
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
                        .Single(c => c.LibraryCardId == card.LibraryCardId);


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
                        .Single(c => c.LibraryCardId == cardId);

                ctx.LibraryCards.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
