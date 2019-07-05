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
        public List<Item> items { get; }

        public StashTab(string name, int size, StashTabTypes stashTabTypes, List<Item> items)
        {
            this.name = name;
            this.size = size;
            this.stashTabTypes = stashTabTypes;
            this.items = items;
        }
    }
}
