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

        public Jewel(JewelBaseTypes jewelBaseTypes, string name, ItemRarity itemRarity, ItemBase itemBase, bool isIdentified, int itemlevel, string[] mods) : 
            base(name, itemRarity, ItemBaseType.Rings, itemBase, isIdentified, itemlevel, null, 0, mods)
        {
            this.jewelBaseTypes = jewelBaseTypes;
        }
    }
}
