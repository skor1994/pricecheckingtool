using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    enum ItemRarity { normal, magic, rare, unique };
    enum ItemBaseType { OneHandedWeapon, TwoHandedWeapon, Jewel, Ring, Amulet, Belt, Gloves, Boots, BodyArmour, Helmet, Shield, Quiver };
    enum ItemBase { shaper, elder, normal };

    abstract class Item
    {
        public string name { get; }
        public ItemRarity itemRarity { get; }
        public ItemBaseType itemBaseType { get; }
        public ItemBase itemBase { get; }
        public bool isIdentified { get; }
        public string[] mods { get; }
        public int itemlevel { get; }
        public Dictionary<char, int> socketsAndColors { get; }
        public int links { get; }
        public string value { get; }

        public Item(string name, ItemRarity itemRarity, ItemBaseType itemBaseType, ItemBase itemBase, bool isIdentified, int itemlevel, Dictionary<char, int> socketsAndColors, int links, string[] mods, string value)
        {
            this.name = name;
            this.itemRarity = itemRarity;
            this.itemBaseType = itemBaseType;
            this.itemBase = itemBase;
            this.isIdentified = isIdentified;
            this.itemlevel = itemlevel;
            this.socketsAndColors = socketsAndColors;
            this.links = links;
            this.mods = mods;
            this.value = value;
        }

    }
}
