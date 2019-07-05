using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    enum StashTabTypes { Normal, Premium, Quad, Currency, Map, Unique, Essence, Fragment, Divination };

    abstract class StashTab
    {
        public string name { get; }
        public int size { get; }
        public StashTabTypes stashTabTypes { get; }

        public StashTab(string name, int size, StashTabTypes stashTabTypes)
        {
            this.name = name;
            this.size = size;
            this.stashTabTypes = stashTabTypes;
        }
    }
}
