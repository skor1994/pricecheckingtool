using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool.Models.Stashes
{
    class EssenceTab : StashTab
    {
        //size of essencetab is irrelevant

        public EssenceTab(string name, List<Item> items) : base(name, 0, StashTabTypes.Essence, items)
        {

        }
    }
}
