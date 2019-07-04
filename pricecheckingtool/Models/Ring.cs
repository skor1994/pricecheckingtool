using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    class Ring : Item
    {
        public Ring(string name, ItemRarity itemRarity, ItemBase itemBase, bool isIdentified, int itemlevel, Dictionary<char, int> socketsAndColors, string[] mods) : 
            base(name, itemRarity, ItemBaseType.Ring, itemBase, isIdentified, itemlevel, socketsAndColors, 0, mods)
        {

        }
    }
}
