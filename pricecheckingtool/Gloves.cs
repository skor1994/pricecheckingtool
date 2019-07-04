using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    enum GlovesBaseTypes { Str, Dex, Int, StrDex, StrInt, DexInt };

    class Gloves : Item
    {
        public GlovesBaseTypes glovesBaseTypes { get; }

        public Gloves(GlovesBaseTypes glovesBaseTypes, string name, ItemRarity itemRarity, ItemBase itemBase, bool isIdentified, int itemlevel, Dictionary<char, int> socketsAndColors, int links, string[] mods) : 
            base(name, itemRarity, ItemBaseType.Gloves, itemBase, isIdentified, itemlevel, socketsAndColors, links, mods)
        {
            this.glovesBaseTypes = glovesBaseTypes;
        }
    }
}
