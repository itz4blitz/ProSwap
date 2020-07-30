using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Models.Post
{
    public class PostListItem
    {
        [Display(Name = "Post ID: ")]
        public int PostId { get; set; }
        [Display(Name = "Owner: ")]
        public string OwnerName { get; set; }
        [Display(Name = "Title: ")]
        public string Title { get; set; }
        [Display(Name = "Body: ")]
        public string Body { get; set; }
        [Display(Name = "Created on: ")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Last modified: ")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
