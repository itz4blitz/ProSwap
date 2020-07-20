using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Data.OfferTypes
{
    public class CurrencyOffer : Offer
    {
        public string CurrencyName { get; set; }
        public decimal PricePerUnit { get; set; }
        public int UnitsAvailable { get; set; }
    }
}
