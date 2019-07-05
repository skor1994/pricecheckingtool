using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    class Belt : Item
    {
        public Belt(string name, ItemRarity itemRarity, ItemBase itemBase, bool isIdentified, int itemlevel, Dictionary<char, int> socketsAndColors, string[] mods, string value) :
            base(name, itemRarity, ItemBaseType.Belt, itemBase, isIdentified, itemlevel, socketsAndColors, 0, mods, value)
        {

        }
    }
}
