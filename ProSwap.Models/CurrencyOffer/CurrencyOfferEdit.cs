using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Models.Offer.OfferType.CurrencyOffer
{
    public class CurrencyOfferEdit : OfferEdit
    {
        [Display(Name = "Unit Price: ")]
        public decimal PricePerUnit { get; set; }
        [Display(Name = "Units Available: ")]
        public int UnitsAvailable { get; set; }
        [Display(Name = "Currency Name: ")]
        public string CurrencyName { get; set; }
    }
}
