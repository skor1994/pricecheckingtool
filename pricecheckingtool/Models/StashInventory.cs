using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    public class StashInventory
    {
        public string numTabs { get; set; }
        public ObservableCollection<Item> items { get; set; } = new ObservableCollection<Item>();
    }
}
