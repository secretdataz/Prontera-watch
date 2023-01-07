using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibProntera.KafraKr
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class KafraItem
    {
        public string server { get; set; }
        public int item_const { get; set; }
        public string name { get; set; }
        public string screen_name { get; set; }
        public int type { get; set; }
        public object weapon_type { get; set; }
        public object price_buy { get; set; }
        public object price_sell { get; set; }
        public int slots { get; set; }
        public object cardillustname { get; set; }
        public object fix { get; set; }
        public object fixname { get; set; }
        public int weight { get; set; }
        public string item_text { get; set; }
        public object move_drop { get; set; }
        public object move_exchange { get; set; }
        public object move_warehouse { get; set; }
        public object move_cart { get; set; }
        public object move_npcsell { get; set; }
        public object move_mail { get; set; }
        public object move_action { get; set; }
        public object move_gwarehouse { get; set; }
        public object extra_info { get; set; }
        public object url1 { get; set; }
        public object url2 { get; set; }
        public object url3 { get; set; }
    }
}
