using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    class NormalTab : StashTab
    {
        public NormalTab(string name, List<Item> items) : base(name, 144, StashTabTypes.Normal, items)
        {

        }
    }
}
