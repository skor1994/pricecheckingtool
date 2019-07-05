using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{

    class StashTab
    {
        public string name { get; }
        public string type { get; }
        public string id { get; }
        public List<Item> items { get; }

        public StashTab(string name, string type, string id, List<Item> items)
        {
            this.name = name;
            this.type = type;
            this.id = id;
            this.items = items;
        }

    }
}
