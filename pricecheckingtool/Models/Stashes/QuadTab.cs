using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    class QuadTab : StashTab
    {
        public QuadTab(string name, List<Item> items) : base(name, 576, StashTabTypes.Quad, items)
        {

        }
    }
}
