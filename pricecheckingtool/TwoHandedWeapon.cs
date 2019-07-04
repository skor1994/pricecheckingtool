using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    enum TwoHandedWeaponBaseTypes { Bows, Staves, Swords, Axes, Maces}

    class TwoHandedWeapon : Item
    {
        public TwoHandedWeaponBaseTypes twoHandedWeaponBaseTypes { get; }

        public TwoHandedWeapon(TwoHandedWeaponBaseTypes twoHandedWeaponBaseTypes, string name, ItemRarity itemRarity, ItemBase itemBase, bool isIdentified, int itemlevel, Dictionary<char, int> socketsAndColors, int links, string[] mods) : 
            base(name, itemRarity, ItemBaseType.TwoHandedWeapon, itemBase, isIdentified, itemlevel, socketsAndColors, links, mods)
        {
            this.twoHandedWeaponBaseTypes = twoHandedWeaponBaseTypes;
        }
    }
}
