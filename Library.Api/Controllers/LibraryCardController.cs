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
{
    [Authorize]
    public class LibraryCardController : ApiController
    {
       private LibraryCardService LibraryCardService()
        {
            var cardId = Guid.Parse(User.Identity.GetUserId());
            var libraryCardService = new LibraryCardService(libraryCardId);
            return libraryCardService;
        }

        public IHttpActionResult Get()
        {
            LibraryCardService libraryCardService = LibraryCardService();
            var cards = libraryCardService.GetCards();
            return Ok(cards);
        }

        public IHttpActionResult Post(LibraryCardCreate card)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateLibraryCardService();

            if (!service.LibraryCardCreate(cards))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Get(int id)
        {
            LibraryCardService libraryCardService = CreateLibraryService();
            var card = libraryCardService.GetCardById(id);
            return Ok(card);
        }

        public IHttpActionResult Put(LibraryCardEdit card)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateLibraryCardService();

            if (!service.UpdateLibraryCard(card))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateLibraryCardService();

            if (!service.DeleteLibraryCard(id))
                return InternalServerError();

            return Ok();
        }
    }
}
