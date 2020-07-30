using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Models.Post
{
    public class PostCreate
    {
        [Required]
        [MinLength(6, ErrorMessage = "There must be atleast 6 characters in this field.")]
        [MaxLength(100, ErrorMessage = "There must be less than 100 characters in this field.")]
        [Display(Name = "Title: ")]
        public string Title { get; set; }
        [Required]
        [MinLength(90, ErrorMessage = "There must be atleast 90 characters in this field.")]
        [MaxLength(5000, ErrorMessage = "There must be less than 5,000 characters in this field.")]
        [Display(Name = "Body: ")]
        public string Body { get; set; }
    }
}
