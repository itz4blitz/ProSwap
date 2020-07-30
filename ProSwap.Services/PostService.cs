using ProSwap.Data;
using ProSwap.Models.Offer;
using ProSwap.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Services
{
    public class PostService
    {
        private readonly Guid _userId;

        public PostService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePost(PostCreate model)
        {
            var entity =
                new Post()
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    Body = model.Body,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Posts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PostListItem> GetPosts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Posts
                        .Select(
                            e =>
                                new PostListItem
                                {
                                    OwnerName = ctx.Users.FirstOrDefault(u => u.Id == e.OwnerId.ToString()).UserName,
                                    PostId = e.PostId,
                                    Title = e.Title,
                                    Body = e.Body,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc,
                                }
                        );

                return query.ToArray();
            }
        }

        public PostDetails GetPostById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Posts
                        .Single(e => e.PostId == id);
                return
                    new PostDetails
                    {
                        PostId = entity.PostId,
                        Title = entity.Title,
                        Body = entity.Body,
                        OwnerName = ctx.Users.FirstOrDefault(u => u.Id == entity.OwnerId.ToString()).UserName,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdatePost(PostEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Posts
                        .Single(e => e.OwnerId == _userId && e.PostId == model.PostId);

                entity.Title = model.Title;
                entity.Body = model.Body;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePost(int postId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Posts
                        .Single(e => e.PostId == postId && e.OwnerId == _userId);

                ctx.Posts.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
