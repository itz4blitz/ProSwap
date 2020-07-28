using ProSwap.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Models.Offer
{
    public class OfferListItem
    {
        [Display(Name = "Offer ID: ")]
        public int OfferId { get; set; }
        public string Owner { get; set; }
        [Display(Name = "Game: ")]
        public string GameName { get; set; }
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
