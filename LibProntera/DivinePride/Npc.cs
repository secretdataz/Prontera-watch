using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibProntera.DivinePride
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Npc
    {
        public int id { get; set; }
        public string name { get; set; }
        public string mapname { get; set; }
        public int job { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public string type { get; set; }
    }
}
