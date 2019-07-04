using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    enum ItemRarity { normal, magic, rare, unique };
    enum ItemBaseType { OneHandedWeapon, TwoHandedWeapon, Jewel, Rings, Amulet, Belt, Gloves, Boots, BodyArmour, Helmet, Shield, Quiver, Flask };

    abstract class Item
    {
        public string name { get; }
        public ItemRarity itemRarity { get; }
        public ItemBaseType itemBaseType { get; }
        public bool isIdentified { get; }
        public string[] mods { get; }
        
        // any properties missing?

        public Item(string name, ItemRarity itemRarity, ItemBaseType itemBaseType, bool isIdentified, string[] mods)
        {
            this.name = name;
            this.itemRarity = itemRarity;
            this.itemBaseType = itemBaseType;
            this.isIdentified = isIdentified;
            this.mods = mods;
        }

    }
}
