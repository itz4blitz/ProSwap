using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Models.Offer.OfferType.CurrencyOffer
{
    public class CurrencyOfferListItem : OfferListItem
    {
        [Display(Name = "Currency: ")]
        public int CurrencyID { get; set; }
        [Display(Name = "Currency Name: ")]
        public string CurrencyName { get; set; }
        [Display(Name = "Unit Price: ")]
        public decimal PricePerUnit { get; set; }
        [Display(Name = "Available Units: ")]
        public int UnitsAvailable { get; set; }
    }
}
