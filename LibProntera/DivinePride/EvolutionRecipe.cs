using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibProntera.DivinePride
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class EvolutionRecipe
    {
        public int RecipeItemId { get; set; }
        public int Amount { get; set; }
    }
}
