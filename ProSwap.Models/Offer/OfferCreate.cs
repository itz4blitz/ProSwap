using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProSwap.Models.Offer
{
    public class OfferCreate
    {
        [Display(Name = "Game: ")]
        public int GameId { get; set; }
        public IEnumerable<SelectListItem> ListOfGames { get; set; }

        [Display(Name = "Title: ")]
        [MinLength(10)]
        public string Title { get; set; }
        [Display(Name = "Details: ")]
        [MinLength(75)]
        public string Body { get; set; }
    }
}
