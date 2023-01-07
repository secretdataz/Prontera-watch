using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibProntera.DivinePride
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ItemMoveInfo
    {
        public bool drop { get; set; }
        public bool trade { get; set; }
        public bool store { get; set; }
        public bool cart { get; set; }
        public bool sell { get; set; }
        public bool mail { get; set; }
        public bool auction { get; set; }
        public bool guildStore { get; set; }
    }
}
