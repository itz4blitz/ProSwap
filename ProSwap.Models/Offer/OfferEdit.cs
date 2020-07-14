using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Models.Offer
{
    public class OfferEdit
    {
        public int OfferId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsAvailable { get; set; }
        public int QuantityAvailable { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}
