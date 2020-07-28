using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Models.Offer.OfferType.AccountOffer
{
    public class AccountOfferCreate : OfferCreate
    {
        public decimal Price { get; set; }
    }
}
