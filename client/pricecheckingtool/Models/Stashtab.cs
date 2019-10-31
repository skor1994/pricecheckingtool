using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace pricecheckingtool
{

    public sealed class StashTab
    {
        public string n { get; set; }
        public int i { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public bool hidden { get; set; }
        public bool selected { get; set; }
        public ObservableCollection<Item> items { get; set; }

        public StashTab()
        {
            items = new ObservableCollection<Item>();
        }
    }
}
