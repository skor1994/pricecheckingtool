using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    enum BootsBaseTypes { Str, Dex, Int, StrDex, StrInt, DexInt };

    class Boots : Item
    {
        public BootsBaseTypes bootsBaseTypes { get; }

        public Boots(BootsBaseTypes bootsBaseTypes, string name, ItemRarity itemRarity, ItemBase itemBase, bool isIdentified, int itemlevel, Dictionary<char, int> socketsAndColors, int links, string[] mods) :
            base(name, itemRarity, ItemBaseType.Boots, itemBase, isIdentified, itemlevel, socketsAndColors, links, mods)
        {
            this.bootsBaseTypes = bootsBaseTypes;
        }
    }
}
