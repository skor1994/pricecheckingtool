using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace pricecheckingtool
{
    public enum ItemRarity { Normal, Magic, Rare, Unique, Gem, Currency, Divination, Quest, Prophecy, Relic };

    public sealed class Item 
    {
        public string name { get; set; }
        public double mean { get; set; }
        public ItemRarity ItemRarity { get; }
        public bool isIdentified { get; }
        List<string> explicitMods { get; }
        public string[] mods { get; }
        public int itemlevel { get; }
        public Dictionary<char, int> socketsAndColors { get; }
        public int links { get; }
        public int stackSize { get; }
        public string typeLine { get; }

        public Item(int itemRarity, string itemName, int itemLvl, bool identified, string typeLine, int stackSize, List<string> explicitMods)
        {
            this.ItemRarity = (ItemRarity)itemRarity;
            this.typeLine = typeLine;
            this.stackSize = stackSize;
            this.explicitMods = explicitMods;
            name = itemName;
            itemlevel = itemLvl;
            isIdentified = identified;
            checkPrice();
      
        }

        public Item()
        {

        }

        private void checkPrice()
        {
            if(ItemRarity == ItemRarity.Prophecy)
            {
                int index = PriceLists.prophecy.FindIndex(i => i.name == typeLine);
                mean = Math.Round(PriceLists.prophecy.ElementAt(index).mean, 1);
            }
            else if (ItemRarity == ItemRarity.Divination)
            {
                int index = PriceLists.card.FindIndex(i => i.name == typeLine);
                mean = Math.Round(PriceLists.card.ElementAt(index).mean, 1);
            }
            else if (ItemRarity == ItemRarity.Currency)
            {
                int index = PriceLists.currency.FindIndex(i => i.name == typeLine);
                if(index < 0)
                {
                    return;
                }
                else
                {
                    mean = Math.Round(PriceLists.currency.ElementAt(index).mean, 1);
                }
            }
        }
    }
}
