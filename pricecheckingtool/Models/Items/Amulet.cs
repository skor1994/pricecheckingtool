using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    class Amulet : Item
    {
        public Amulet(string name, ItemRarity itemRarity, ItemBase itemBase, bool isIdentified, int itemlevel, string[] mods) :
            base(name, itemRarity, ItemBaseType.Amulet, itemBase, isIdentified, itemlevel, null, 0, mods)
        {

        }
        
    }
}
