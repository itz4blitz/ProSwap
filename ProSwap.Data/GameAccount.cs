using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Data
{
    public class GameAccount : Game
    {
        public int GameAccountId { get; set; }
        public Guid OwnerId { get; set; }
    }
}
