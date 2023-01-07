using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibProntera.DivinePride
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Pet
    {
        public int monsterId { get; set; }
        public string monsterName { get; set; }
        public int chance { get; set; }
        public int intimacy_fed { get; set; }
        public int intimacy_overfed { get; set; }
        public int intimacy_starve { get; set; }
        public int intimacy_die { get; set; }
        public int hunger_fed { get; set; }
        public int hunger_interval { get; set; }
        public bool performance { get; set; }
        public int accessoryId { get; set; }
        public string accessoryName { get; set; }
        public int foodId { get; set; }
        public string foodName { get; set; }
        public int captureId { get; set; }
        public string captureName { get; set; }
        public int eggId { get; set; }
        public string eggName { get; set; }
        public int evolutionSourceItemId { get; set; }
        public string evolutionSourceName { get; set; }
        public int evolutionTargetItemId { get; set; }
        public string evolutionTargetName { get; set; }
        public List<EvolutionRecipe> evolutionRecipes { get; set; }
    }
}
