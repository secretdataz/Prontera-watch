using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibProntera.DivinePride
{
    //a15a6cfb83cf31d8ce96c141aa37c863
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class DivinePrideItem
    {
        //public object classNum { get; set; }
        public List<Set> sets { get; set; }
        public List<SoldBy> soldBy { get; set; }
        public int id { get; set; }
        public string aegisName { get; set; }
        public string name { get; set; }
        public string unidName { get; set; }
        public string resName { get; set; }
        public string unidResName { get; set; }
        public string description { get; set; }
        public string unidDescription { get; set; }
        public int slots { get; set; }
        public string setname { get; set; }
        public int itemTypeId { get; set; }
        public int itemSubTypeId { get; set; }
        public List<ItemSummonInfoContainedIn> itemSummonInfoContainedIn { get; set; }
        //public List<object> itemSummonInfoContains { get; set; }
        public int attack { get; set; }
        public int defense { get; set; }
        public double weight { get; set; }
        public int requiredLevel { get; set; }
        public int limitLevel { get; set; }
        public int itemLevel { get; set; }
        public int job { get; set; }
        //public object compositionPos { get; set; }
        //public object attribute { get; set; }
        //public object location { get; set; }
        public int locationId { get; set; }
        //public object accessory { get; set; }
        public int price { get; set; }
        public int range { get; set; }
        public int matk { get; set; }
        public int gender { get; set; }
        public bool refinable { get; set; }
        public bool indestructible { get; set; }
        public ItemMoveInfo itemMoveInfo { get; set; }
        //public List<object> rewardForAchievement { get; set; }
        public string cardPrefix { get; set; }
        public List<Pet> pets { get; set; }
        public bool hasScript { get; set; }
    }
}
