using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Data
{
    public class Currency : Game
    {
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitQuantity { get; set; }
        public double UnitLowValue { get; set; }
        public double UnitHighValue { get; set; }
        public double UnitMedianValue { get; set; }
        public double UnitAverage { get; set; }
    }
}
