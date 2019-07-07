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
    enum ItemRarity { Normal, Magic, Rare, Unique, Gem, Currency, Divination, Quest, Prophecy, Relic };
    enum ItemBaseType { OneHandedWeapon, TwoHandedWeapon, Jewel, Ring, Amulet, Belt, Gloves, Boots, BodyArmour, Helmet, Shield, Quiver };
    enum ItemBase { shaper, elder, normal };

    class Item 
    {
        public string name { get; set; }
        public double mean { get; set; }
        public ItemRarity itemRarity { get; }
        public ItemBaseType itemBaseType { get; }
        public ItemBase itemBase { get; }
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
            this.itemRarity = (ItemRarity)itemRarity;
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
            if(itemRarity == ItemRarity.Prophecy)
            {
                int index = PriceLists.prophecy.FindIndex(i => i.name == typeLine);
                mean = PriceLists.prophecy.ElementAt(index).mean;
            }
            else if (itemRarity == ItemRarity.Divination)
            {
                int index = PriceLists.card.FindIndex(i => i.name == typeLine);
                mean = PriceLists.card.ElementAt(index).mean;
            }
            else if (itemRarity == ItemRarity.Currency)
            {
                int index = PriceLists.currency.FindIndex(i => i.name == typeLine);
                mean = PriceLists.currency.ElementAt(index).mean;
            }
        }
    }
}
