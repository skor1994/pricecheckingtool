using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    class Quiver : Item
    {
        public Quiver(string name, ItemRarity itemRarity, ItemBase itemBase, bool isIdentified, int itemlevel, string[] mods) :
            base(name, itemRarity, ItemBaseType.Quiver, itemBase, isIdentified, itemlevel, null, 0, mods)
        {

        }
    }
}
