using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    enum JewelBaseTypes { Crimson, Viridian, Cobalt, Prismatic, Murderous, Searching, Hypnotic, Ghastly };

    class Jewel : Item
    {
        public JewelBaseTypes jewelBaseTypes { get; }

        public Jewel(JewelBaseTypes jewelBaseTypes, string name, ItemRarity itemRarity, ItemBase itemBase, bool isIdentified, int itemlevel, string[] mods, string value) : 
            base(name, itemRarity, ItemBaseType.Jewel, itemBase, isIdentified, itemlevel, null, 0, mods, value)
        {
            this.jewelBaseTypes = jewelBaseTypes;
        }
    }
}
