using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    class PremiumTab : StashTab
    {
        public PremiumTab(string name, List<Item> items) : base(name, 144, StashTabTypes.Premium, items)
        {

        }
    }
}
