using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    enum HelmetBaseTypes { Str, Dex, Int, StrDex, StrInt, DexInt };

    class Helmet : Item
    {
        public HelmetBaseTypes helmetBaseTypes { get; }

        public Helmet(HelmetBaseTypes helmetBaseTypes, string name, ItemRarity itemRarity, ItemBase itemBase, bool isIdentified, int itemlevel, Dictionary<char, int> socketsAndColors, int links, string[] mods) :
            base(name, itemRarity, ItemBaseType.Helmet, itemBase, isIdentified, itemlevel, socketsAndColors, links, mods)
        {
            this.helmetBaseTypes = helmetBaseTypes;
        }
    }
}
