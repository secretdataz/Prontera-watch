using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibProntera.DivinePride
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Set
    {
        public string name { get; set; }
        public List<Item> items { get; set; }
    }
}
