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
       private LibraryCardService CreateLibraryCardService()
        {
            var libraryCardId = Guid.Parse(User.Identity.GetUserId());
            var libraryCardService = new LibraryCardService(libraryCardId);
            return libraryCardService;
        }

        public IHttpActionResult Get()
        {
            LibraryCardService libraryCardService = CreateLibraryCardService();
            var libraryCards = libraryCardService.GetLibraryCards();
            return Ok(libraryCards);
        }

        public IHttpActionResult Post(LibraryCardCreate libraryCard)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateLibraryCardService();

            if (!service.CreateLibraryCard(libraryCard))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Get(int id)
        {
            LibraryCardService libraryCardService = CreateLibraryCardService();
            var card = libraryCardService.GetLibraryCardById(id);
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
