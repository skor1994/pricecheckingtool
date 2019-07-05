using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    class Fragment : StashTab
    {
        //size of fragement tab is irrelevant

        public Fragment(string name, List<Item> items) : base(name, 0, StashTabTypes.Fragment, items)
        {

        }
    }
}
