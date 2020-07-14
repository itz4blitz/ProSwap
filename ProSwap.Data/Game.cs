using ProSwap.Data;
using System.Collections.Generic;
using System.Data.Entity;

namespace ProSwap.Data
{
    public class Game
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
    }
}
