using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Models.Offer
{
    public class OfferDetail
    {
        public int OfferId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public decimal PricePerUnit { get; set; }
        public int UnitsAvailable { get; set; }
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Created")]
        public DateTimeOffset? ModifiedUtc { get; set; }

    }
}
