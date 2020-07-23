using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Data
{
    public class Post
    {
        public int PostId { get; set; }
        public Guid OwnerId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }

    }
}
