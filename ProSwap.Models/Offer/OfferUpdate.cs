using ProSwap.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Models.Offer
{
    public class OfferUpdate
    {
        [Display(Name = "ID: ")]
        public int OfferId { get; set; }
        [Display(Name = "Title: ")]
        public string Title { get; set; }
        [Display(Name = "Details: ")]
        public string Body { get; set; }
        [Display(Name = "Active: ")]
        public bool IsActive { get; set; }

    }
}
