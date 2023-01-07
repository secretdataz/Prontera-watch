using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibProntera.KafraKr
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class KafraItemHistory
    {
        public int id { get; set; }
        public int goods_idx { get; set; }
        public string goods_name { get; set; }
        public int item_const { get; set; }
        public string item_resname { get; set; }
        public string item_screen_name { get; set; }
        public int refine_num { get; set; }
        public int socket { get; set; }
        public int dealdate { get; set; }
        public int itemprice { get; set; }
        public int svr_const { get; set; }
        public string slot1 { get; set; }
        public string slot2 { get; set; }
        public string slot3 { get; set; }
        public string slot4 { get; set; }
        public int slot1_const { get; set; }
        public int slot2_const { get; set; }
        public int slot3_const { get; set; }
        public int slot4_const { get; set; }
        public string slot1_resname { get; set; }
        public string slot2_resname { get; set; }
        public string slot3_resname { get; set; }
        public string slot4_resname { get; set; }
        public int report { get; set; }
    }
}
