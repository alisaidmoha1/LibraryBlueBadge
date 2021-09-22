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
{ [Authorize]
    public class BookController : ApiController
    {
        private BookService CreateBookService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var bookService = new BookService(userId);
            return bookService;
        }

        [ActionName("Get Books by Library Card ID")]
        public IHttpActionResult GetBooksByLibraryCardId(int libraryId)
        {
            var service = CreateBookService();
            var book = service.GetAllBooksByLibraryCardId(libraryId);            
            return Ok(book);

        }

        [ActionName("Get All Books")]
        public IHttpActionResult GetBooks()
        {
            BookService bookService = CreateBookService();
            var books = bookService.GetBooks();

            if (books == null)
                return BadRequest("There are no books in the library database!");

            return Ok(books);
        }

        [ActionName("Create Book")]
        public IHttpActionResult CreateBook(BookCreate book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateBookService();

            if (!service.CreateBook(book))
                return InternalServerError();

            return Ok("you successfuly created a book");
        }

        [ActionName("Add book to library card")]
        public IHttpActionResult PostBookToLibraryCard (int bookid, int libraryid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateBookService();

            var ctx = new ApplicationDbContext();

            LibraryCard books = ctx.LibraryCards.Find(libraryid);

            if (books.ListOfBooks.Count > 3)
                return BadRequest("You reached the maximum books you can borrow");

            Book book = ctx.Books.Find(bookid);

            if (book.Quantity == 0)
                return BadRequest("The book you looking for is out of stock");


            service.AddBooksToLibrarayCard(bookid, libraryid);           

            return Ok($"You added Book Id No: {book.BookId} to Library Card Id: {libraryid}");
        }

        
        [ActionName("Reserve Book to Library Card")]
        public IHttpActionResult ReserveBook(LibraryCardBookReservation reserve)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateBookService();

            var ctx = new ApplicationDbContext();

            LibraryCard books = ctx.LibraryCards.Find(reserve.LibraryCardId);

            if (books.ListOfBooks.Count > 3)
                return BadRequest("You reached the maximum books you can borrow");

            Book book = ctx.Books.Find(reserve.BookId);

            if (book.Quantity == 0)
                return BadRequest("The book you looking for is out of stock");


            service.ReserveBooksToLibrarayCard(reserve);

            return Ok();
        }

        [ActionName("Remove book from library card")]
        public IHttpActionResult DeleteBookFromLibraryCard (ReturnBook book)
        {
            var service = CreateBookService();

            service.RemoveBooksFromLibraryCard(book);

            return Ok($"You removed Book Id No: {book.BookId} from Library Card Id: {book.LibraryCardId}");
        }



        [ActionName("Return book from library card")]
        public IHttpActionResult Delete (ReturnBook book)
        {
            var service = CreateBookService();

            service.RemoveBooksFromLibraryCard(book);


            var ctx = new ApplicationDbContext();

            Checkout checkout = ctx.Checkouts.Find(book.BookId);

            var daysLate = book.ReturnDate - checkout.DueDate;

            if (daysLate.Days > 0)
            {

                int dayCount = 0;

                for (int i = 1; i <= daysLate.Days; i++)
                {
                    if (checkout.DueDate.AddDays(i).DayOfWeek != DayOfWeek.Sunday)
                        dayCount++;
                }

                Decimal fine = dayCount * 0.10m;


                return Ok($"You returned Book Id No: {book.BookId} and you should pay ${fine} because you were late {daysLate.Days} days");
            }

            return Ok($"You returned Book Id No: {book.BookId}");
        }

        [ActionName("Get Book by ID")]
        public IHttpActionResult Get(int id)
        {
            BookService bookService = CreateBookService();
            var book = bookService.GetBookById(id);
            return Ok(book);
        }

        [ActionName("Edit Book by ID")]
        public IHttpActionResult Put(BookEdit book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateBookService();

            if (!service.UpdateBook(book))
                return InternalServerError();

            return Ok($"You updated book Id No: {book.BookId}");
        }

        [ActionName("Restock Books")]
        public IHttpActionResult RestockBooks(BookAmount amount)
        {
            var service = CreateBookService();

            if (!service.RestockBooks(amount))
                return InternalServerError();

            return Ok();
        }

        [ActionName("Delete Book from Database")]
        public IHttpActionResult Delete (int id)
        {
            var service = CreateBookService();

            if (!service.DeleteBook(id))
                return InternalServerError();

            return Ok($"You deleted Book Id No: {id}");
        }

            
    }
}
