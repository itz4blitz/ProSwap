using Microsoft.AspNet.Identity;
using ProSwap.Models.Game;
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
    public class GameController : ApiController
    {
        private GameService CreateGameService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var gameService = new GameService(userId);
            return gameService;
        }

        public IHttpActionResult Get()
        {
            GameService gameService = CreateGameService();
            var game = gameService.GetAllGames();
            return Ok(game);
        }

        public IHttpActionResult Get(int id)
        {
            GameService gameService = CreateGameService();
            var games = gameService.GetGameById(id);
            return Ok(games);
        }

        public IHttpActionResult Post(GameCreate game)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateGameService();

            if (!service.CreateGame(game))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Put(GameEdit game)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateGameService();

            if (!service.UpdateGame(game))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateGameService();

            if (!service.DeleteGame(id))
                return InternalServerError();

            return Ok();
        }
    }
}
