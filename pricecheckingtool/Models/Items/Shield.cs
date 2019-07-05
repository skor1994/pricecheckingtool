using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    enum ShieldBaseTypes { Str, Dex, Int, StrDex, StrInt, DexInt };

    class Shield : Item
    {
        public ShieldBaseTypes shieldBaseTypes { get; }

        public Shield(ShieldBaseTypes shieldBaseTypes, string name, ItemRarity itemRarity, ItemBase itemBase, bool isIdentified, int itemlevel, Dictionary<char, int> socketsAndColors, int links, string[] mods) :
            base(name, itemRarity, ItemBaseType.Shield, itemBase, isIdentified, itemlevel, socketsAndColors, links, mods)
        {
            this.shieldBaseTypes = shieldBaseTypes;
        }
    }
}
