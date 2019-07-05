using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    enum BodyArmourBaseTypes { Str, Dex, Int, StrDex, StrInt, DexInt, StrDexInt };

    class BodyArmour : Item
    {
        public BodyArmourBaseTypes bodyArmourBaseTypes { get; }

        public BodyArmour(BodyArmourBaseTypes bodyArmourBaseTypes, string name, ItemRarity itemRarity, ItemBase itemBase, bool isIdentified, int itemlevel, Dictionary<char, int> socketsAndColors, int links, string[] mods) :
            base(name, itemRarity, ItemBaseType.BodyArmour, itemBase, isIdentified, itemlevel, socketsAndColors, links, mods)
        {
            this.bodyArmourBaseTypes = bodyArmourBaseTypes;
        }
    }
}
