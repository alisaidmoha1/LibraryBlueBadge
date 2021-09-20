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

        public IHttpActionResult GetBooksByLibraryCardId(int libraryId)
        {
            var service = CreateBookService();
            var book = service.GetAllBooksByLibraryCardId(libraryId);
            return Ok(book);

        }

        public IHttpActionResult Get()
        {
            BookService bookService = CreateBookService();
            var books = bookService.GetBooks();

            if (books == null)
                return BadRequest("There are no books in the library database!");

            return Ok(books);
        }

        public IHttpActionResult Post(BookCreate book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateBookService();

            if (!service.CreateBoook(book))
                return InternalServerError();

            return Ok("you successfuly created a book");
        }

        public IHttpActionResult Post (int bookid, int libraryid)
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

            return Ok();
        }

        
        [ActionName("ReserveBook")]
        public IHttpActionResult ReserveBook(int bookid, int libraryid)
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

            return Ok();
        }

        public IHttpActionResult Delete (int bookId, int libraryId)
        {
            var service = CreateBookService();

            service.RemoveBooksFromLibraryCard(bookId, libraryId);                

            return Ok($"You removed Book Id No: {bookId} from Library Card Id: {libraryId}");
        }

        public IHttpActionResult Get(int id)
        {
            BookService bookService = CreateBookService();
            var book = bookService.GetBookById(id);
            return Ok(book);
        }

        [Route("api/Book/{id}/Update")]
        public IHttpActionResult Put(BookEdit book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateBookService();

            if (!service.UpdateBook(book))
                return InternalServerError();

            return Ok($"You updated book Id No: {book.BookId}");
        }

        [Route("api/Book/{id}/Restock")]
        public IHttpActionResult Put(BookAmount amount)
        {
            var service = CreateBookService();

            if (!service.RestockBooks(amount))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete (int id)
        {
            var service = CreateBookService();

            if (!service.DeleteBook(id))
                return InternalServerError();

            return Ok($"You deleted Book Id No: {id}");
        }

            
    }
}
