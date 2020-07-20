using ProSwap.Data;
using ProSwap.Models;
using ProSwap.Models.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Services
{
    public class GameService
    {
        private readonly Guid _userId;

        public GameService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateGame(GameCreate model)
        {
            var entity =
                new Game()
                {
                    Name = model.GameName
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Games.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<GameListItem> GetAllGames()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Games
                        .Select(
                            e =>
                                new GameListItem
                                {
                                    GameId = e.ID,
                                    GameName = e.Name
                                }
                        );

                return query.ToArray();
            }
        }
        public GameDetails GetGameById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Games
                        .Single(e => e.ID == id);
                return
                    new GameDetails
                    {
                        GameId = entity.ID,
                        GameName = entity.Name
                    };
            }
        }

        public bool UpdateGame(GameEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Games
                        .Single(e => e.Name == model.GameName);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteGame(int gameId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Games
                        .Single(e => e.ID == gameId);

                ctx.Games.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
