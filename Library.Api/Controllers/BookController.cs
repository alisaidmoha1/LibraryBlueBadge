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

        public IHttpActionResult Get()
        {
            BookService bookService = CreateBookService();
            var books = bookService.GetBooks();

            if (books == null)
                return BadRequest("There are no books in the library database!");

            return Ok(books);
        }

        public IHttpActionResult Post (BookCreate book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateBookService();

            if (!service.CreateBoook(book))
                return InternalServerError();

            return Ok("you successfuly created a book");
        }

        public IHttpActionResult Get(int id)
        {
            BookService bookService = CreateBookService();
            var book = bookService.GetBookById(id);
            return Ok(book);
        }

        public IHttpActionResult Put(BookEdit book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateBookService();

            if (!service.UpdateBook(book))
                return InternalServerError();

            return Ok($"You updated book Id No: {book.BookId}");
        }

        public IHttpActionResult Put (BookEdit book, [FromUri] int amount)
        {
            var service = CreateBookService();

            if (!service.RestockBooks(book, amount))
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
