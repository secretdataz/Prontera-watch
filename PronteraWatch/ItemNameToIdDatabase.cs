using Quickenshtein;    
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace PronteraWatch
{
    public class ItemNameToIdDatabase
    {
        private string DbPath;
        private List<ItemNameToIdMap> itemNameToIdMaps = new List<ItemNameToIdMap>();

        public ItemNameToIdDatabase(string DbPath)
        {
            this.DbPath = DbPath;
            LoadDb();
        }

        public void LoadDb()
        {
            string[] Lines = File.ReadAllLines(DbPath);
            foreach (string Line in Lines)
            {
                if (string.IsNullOrEmpty(Line))
                {
                    continue;
                }
                string[] Parts = Line.Split("#", StringSplitOptions.RemoveEmptyEntries);
                if (Parts.Length < 2)
                {
                    continue;
                }
                itemNameToIdMaps.Add(new ItemNameToIdMap(uint.Parse(Parts[0]), Parts[1].Replace('_', ' ')));
            }
        }

        public uint ResolveName(string Name)
        {
            uint TopId = 0;
            int LeastDist = 99999; // Should be enough

            foreach(var x in itemNameToIdMaps)
            {
                var dist = x.CompareDistance(Name);
                if (x.Name.ToLower().StartsWith(Name.ToLower()) && dist <= LeastDist)
                {
                    if (dist < LeastDist || x.Id > TopId)
                        TopId = x.Id;
                    LeastDist = dist;
                }
            }

            return TopId;
        }
    }

    class ItemNameToIdMap
    {
        public string Name;
        public uint Id;

        public ItemNameToIdMap(uint Id, string Name)
        {
            this.Name = Name;
            this.Id = Id;
        }

        public int CompareDistance(string str)
        {
            return Levenshtein.GetDistance(this.Name, str);
        }
    }
}
