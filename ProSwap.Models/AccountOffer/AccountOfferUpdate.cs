using ProSwap.Models.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Models.AccountOffer
{
    public class AccountOfferUpdate : OfferUpdate
    {
        public decimal Price { get; set; }
    }
}
