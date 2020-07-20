using ProSwap.Data;
using ProSwap.Data.OfferTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Models.Offer
{
    public class OfferCreate
    {
        [Display(Name = "Game: ")]
        [Required]
        public int GameId { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "There must be atleast 6 characters in this field.")]
        [MaxLength(100, ErrorMessage = "There must be less than 100 characters in this field.")]
        [Display(Name = "Title: ")]
        public string Title { get; set; }

        [MinLength(75, ErrorMessage = "There must be atleast 75 characters in this field.")]
        [MaxLength(6000, ErrorMessage = "There must be less than 6000 characters in this field.")]
        [Required]
        [Display(Name = "Details: ")]
        public string Body { get; set; }
    }
}
