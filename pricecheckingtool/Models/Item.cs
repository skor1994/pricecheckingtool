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
        public ItemRarity itemRarity { get; set; }
        public bool identified { get; set; }
        List<string> explicitMods { get; set; }
        List<string> implicitMods { get; set; }
        public int ilvl { get; set; }
        public int stackSize { get; set; }
        public string typeLine { get; set; }
        public int frameType { get; set; }
        public int frame { get; set; } // poe.watch
        public string id { get; set; }

        public Item()
        {

        }

        public ItemRarity ItemRarity
        {
            get { return itemRarity; }
            set
            {
                itemRarity = (ItemRarity) frameType;
            }
        }

        private void checkPrice()
        {
            if(ItemRarity == ItemRarity.Prophecy)
            {
                int index = PriceLists.prophecy.FindIndex(i => i.name == typeLine);
                if (index < 0)
                {
                    return;
                }
                else
                {
                    mean = Math.Round(PriceLists.prophecy.ElementAt(index).mean, 1);
                }           
            }
            else if (ItemRarity == ItemRarity.Divination)
            {
                int index = PriceLists.card.FindIndex(i => i.name == typeLine);
                if (index < 0)
                {
                    return;
                }
                else
                {
                    mean = Math.Round(PriceLists.card.ElementAt(index).mean, 1);
                }                
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
