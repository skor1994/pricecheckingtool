using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    class DivinationTab : StashTab
    {
        // size of divination tab is irrelevant

        public DivinationTab(string name, List<Item> items) : base(name, 0, StashTabTypes.Divination, items)
        {

        }
    }
}
