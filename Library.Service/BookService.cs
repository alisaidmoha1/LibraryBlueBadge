using Library.Data;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Service
{
    public class BookService
    {
        private readonly Guid _userId;

        public BookService(Guid userId)
        {
            _userId = userId;
        }

        public void AddBooksToLibrarayCard(int bookId, int libraryCardId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                
                var foundBook = ctx.Books.Single(b => b.BookId == bookId);
                var foundLibraryCard = ctx.LibraryCards.Single(b => b.LibraryCardId == libraryCardId);
                foundBook.ListOfLibraryCards.Add(foundLibraryCard);
                foundBook.Quantity--;
                var result = ctx.SaveChanges() == 1;
                
            }
        }
        public void RemoveBooksFromLibraryCard(int bookId, int libraryCardId)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var foundBook = ctx.Books.Single(b => b.BookId == bookId);
                var foundLibraryCard = ctx.LibraryCards.Single(b => b.LibraryCardId == libraryCardId);
                foundBook.ListOfLibraryCards.Remove(foundLibraryCard);
                foundBook.Quantity++;
                var result = ctx.SaveChanges() == 1;

            }
        }

        public void ReserveBooksToLibrarayCard(int bookId, int libraryCardId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                DateTime pickUpDate = DateTime.UtcNow.AddDays(7); 
                var foundBook = ctx.Books.Single(b => b.BookId == bookId);
                var foundLibraryCard = ctx.LibraryCards.Single(b => b.LibraryCardId == libraryCardId);
                foundBook.ListOfLibraryCards.Add(foundLibraryCard);
                foundBook.Quantity--;
                var result = ctx.SaveChanges() == 1;
                
            }
        }

        public void RemoveBooksReserveFromLibraryCard(int bookId, int libraryCardId)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var foundBook = ctx.Books.Single(b => b.BookId == bookId);
                var foundLibraryCard = ctx.LibraryCards.Single(b => b.LibraryCardId == libraryCardId);
                foundBook.ListOfLibraryCards.Remove(foundLibraryCard);
                foundBook.Quantity++;
                var result = ctx.SaveChanges() == 1;

            }
        }


        public IEnumerable<BookList> GetAllBooksByLibraryCardId(int libraryCardId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var foundBooks = ctx.LibraryCards.Single(l => l.LibraryCardId == libraryCardId).ListOfBooks
                    .Select(e => new BookList
                    {
                        BookId = e.BookId,
                        ISBN = e.ISBN,
                        AuthorName = e.AuthorName,
                        PublishedDate = e.PublishedDate,

                    }

                        );
                return foundBooks.ToArray();
            }
        }

        public bool CreateBoook(BookCreate book)
        {
            var entity = new Book()
            {                
                AdminId = _userId,
                Title = book.Title,
                ISBN = book.ISBN,
                AuthorName = book.AuthorName,
                PublishedDate = book.PublishedDate,
                Quantity = book.Quantity,
            };
            
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Books.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BookList> GetBooks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Books.Where(e => e.AdminId == _userId)
                    .Select(
                    e =>
                    new BookList
                    {
                        BookId = e.BookId,
                        Title = e.Title,
                        ISBN = e.ISBN,
                        AuthorName = e.AuthorName,
                        PublishedDate = e.PublishedDate,
                        Quantity = e.Quantity,
                    }

                    );

                return query.ToArray();
            }
        }

        public BookList GetBookById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Books.Single(e => e.BookId == id && e.AdminId == _userId);

                return new BookList
                {
                    BookId = entity.BookId,
                    Title = entity.Title,
                    ISBN = entity.ISBN,
                    AuthorName = entity.AuthorName,
                    PublishedDate = entity.PublishedDate,
                    Quantity = entity.Quantity
                };
            }
        }

        

        public bool UpdateBook(BookEdit book)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Books.Single(e => e.BookId == book.BookId && e.AdminId == _userId);

                entity.Title = book.Title;
                entity.ISBN = book.ISBN;
                entity.AuthorName = book.AuthorName;
                entity.PublishedDate = book.PublishedDate;

                return ctx.SaveChanges() == 1;
            }
        }


        public bool RestockBooks(BookAmount restock)
        {
            using (var ctx = new ApplicationDbContext())
            {

                Book entity = ctx.Books.Single(e => e.BookId == restock.BookId && e.AdminId == _userId);

                entity.Quantity += restock.Amount;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBook(int bookId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Books.Single(e => e.BookId == bookId && e.AdminId == _userId);

                ctx.Books.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
