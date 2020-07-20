using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Data.OfferTypes
{
    public class ServiceOffer : Offer
    {
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public decimal Price { get; set; }
        public int DaysTillComplete { get; set; }
        public DateTimeOffset ServiceStartDate { get; set; }
        public DateTimeOffset ServiceEndDate { get; set; }
    }
}
