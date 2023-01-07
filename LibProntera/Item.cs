using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibProntera
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double PastAveragePrice { get; set; }
        public int LatestSoldPrice { get; set; }
        public int OldestSoldPrice { get; set; }
        public DateTime OldestSoldFound { get; set; }
        public DateTime LatestSoldFound { get; set; }
        public int SoldDealsFound { get; set; }
        public int CurrentCheapestPrice { get; set; }
        public double CurrentAveragePrice { get; set; }
    }
}
