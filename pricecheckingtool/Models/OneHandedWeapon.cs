using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    enum OneHandedWeaponBaseTypes { Claws, Daggers, Wands, Swords, Axes, Maces, Sceptres };

    class OneHandedWeapon : Item
    {
        public OneHandedWeaponBaseTypes oneHandedWeaponBaseTypes { get; }

        public OneHandedWeapon(OneHandedWeaponBaseTypes oneHandedWeaponBaseTypes, string name, ItemRarity itemRarity, ItemBase itemBase, bool isIdentified, int itemlevel, Dictionary<char, int> socketsAndColors, int links, string[] mods) :
            base(name, itemRarity, ItemBaseType.OneHandedWeapon, itemBase, isIdentified, itemlevel, socketsAndColors, links, mods)
        {
            this.oneHandedWeaponBaseTypes = oneHandedWeaponBaseTypes;
        }
    }
}
