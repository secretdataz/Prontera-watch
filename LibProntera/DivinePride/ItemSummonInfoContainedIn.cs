using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibProntera.DivinePride
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ItemSummonInfoContainedIn
    {
        public string internalType { get; set; }
        public int Type { get; set; }
        public int sourceId { get; set; }
        public string sourceName { get; set; }
        public int targetId { get; set; }
        public string targetName { get; set; }
        public int count { get; set; }
        public int totalOfSource { get; set; }
        public string summonType { get; set; }
        public int chance { get; set; }
    }
}
