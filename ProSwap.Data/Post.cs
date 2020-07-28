using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Data
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        [MaxLength(175, ErrorMessage = "There are more than the 175 character maximum in this field.")]
        public string Title { get; set; }
        [Required]
        [MaxLength(5000, ErrorMessage = "There are more than the 5000 character maximum in this field.")]
        public string Body { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
