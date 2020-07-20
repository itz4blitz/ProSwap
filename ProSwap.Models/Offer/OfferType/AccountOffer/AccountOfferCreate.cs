using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Models.Offer.OfferType.AccountOffer
{
    public class AccountOfferCreate : OfferCreate
    {
        public int OfferId { get; set; }
        public decimal Price { get; set; }
    }
}
