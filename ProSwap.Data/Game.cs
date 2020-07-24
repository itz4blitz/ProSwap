using ProSwap.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace ProSwap.Data
{
    public class Game
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Game name: ")]
        public string Name { get; set; }
        [Display(Name="Currency: ")]
        public string CurrencyName { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
    }
}
