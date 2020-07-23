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
    public class PostService { 

        private readonly Guid _userId;

    public PostService(Guid userId)
    {
        _userId = userId;
    }

    public bool PostCreate(PostCreate model)
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

    public IEnumerable<PostListItem> GetPostsByDateDescending()
    {
        using (var ctx = new ApplicationDbContext())
        {
            var query =
                ctx
                    .Posts.OrderByDescending(e => e.CreatedUtc)
                    .Select(
                        e =>
                            new PostListItem
                            {
                                PostId = e.PostId,
                                Title = e.Title,
                                Body = e.Body,
                                OwnerId = e.OwnerId,
                                CreatedUtc = e.CreatedUtc,
                                ModifiedUtc = e.ModifiedUtc
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
                    OwnerId = entity.OwnerId,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc
                };
        }
    }

    public bool PostUpdate(PostUpdate model)
    {
        using (var ctx = new ApplicationDbContext())
        {
            var entity =
                ctx
                    .Posts
                    .Single(e => e.PostId == model.PostId && e.OwnerId == _userId);

            entity.Title = model.Title;
            entity.Body = model.Body;
            entity.ModifiedUtc = DateTimeOffset.UtcNow;

            return ctx.SaveChanges() == 1;
        }

    }

    public bool PostDelete(int postId)
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