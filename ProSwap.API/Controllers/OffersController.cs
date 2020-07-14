using Microsoft.AspNet.Identity;
using ProSwap.Models.Offer;
using ProSwap.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProSwap.API.Controllers
{
    [Authorize]
    public class OffersController : ApiController
    {

        private OfferService CreateOfferService()
        {
            var userId = User.Identity.GetUserId();
            var offerService = new OfferService(Guid.Parse(userId));
            return offerService;
        }

        // api/Offer/GetOfferList
        [Route("GetAllPosts")]
        public IHttpActionResult Get()
        {
            OfferService offerService = CreateOfferService();
            var offers = offerService.GetOffers();
            return Ok(offers);
        }

        // api/Offer/GetJobPostById
        [Route("GetOfferById")]
        public IHttpActionResult Get(int id)
        {
            OfferService offerService = CreateOfferService();
            var offer = offerService.GetOfferById(id);

            if (offer == null)
                return NotFound();

            return Ok(offer);
        }

        // api/Freelancer/GetOfferByGameId
        [Route("GetByGameId")]
        public IHttpActionResult GetOfferByGameId(int gameId)
        {
            OfferService offerService = CreateOfferService();
            var offers = offerService.GetOfferByGameId(gameId);
            return Ok(offers);
        }


        public IHttpActionResult Post(OfferCreate offer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOfferService();

            if (!service.OfferCreate(offer))
                return InternalServerError();

            return Ok();
        }

        // api/JobPost/Update
        [Route("UpdateById")]
        public IHttpActionResult Put(OfferEdit offerId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOfferService();

            if (!service.OfferUpdate(offerId))
                return InternalServerError();

            return Ok();
        }

        // api/JobPost/Delete
        [Route("DeleteById")]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateOfferService();

            if (!service.OfferDelete(id))
                return InternalServerError();

            return Ok();
        }
    }
}
