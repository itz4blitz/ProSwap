using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Models.Offer.OfferType.ServiceOffer
{
    public class ServiceOfferListItem : OfferListItem
    {
        [Display(Name = "Service Name: ")]
        public string ServiceName { get; set; }
        [Display(Name = "Service Description: ")]
        public string ServiceDescription { get; set; }
        [Display(Name = "Price: ")]
        public decimal Price { get; set; }
        [Display(Name = "Days To Complete: ")]
        public int DaysToComplete { get; set; }
    }
}
