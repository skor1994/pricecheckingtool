using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    class UniqueTab : StashTab
    {
        // Size of uniquetab is irrelevant

        public UniqueTab(string name, List<Item> items) : base(name, 0, StashTabTypes.Unique, items)
        {

        }
    }
}
