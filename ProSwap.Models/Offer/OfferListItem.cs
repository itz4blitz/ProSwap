using ProSwap.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Models.Offer
{
    public class OfferListItem
    {
        [Display(Name = "Offer ID: ")]
        public int OfferId { get; set; }
        [Display(Name = "Game: ")]
        public int GameId { get; set; }
        [Display(Name = "Created on: ")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Edited on: ")]
        public DateTimeOffset? ModifiedUtc { get; set; }
        [Display(Name = "Active: ")]
        public bool IsActive { get; set; }
        [Display(Name = "Title: ")]
        public string Title { get; set; }
        [Display(Name = "Details: ")]
        public string Body { get; set; }
    }
}
