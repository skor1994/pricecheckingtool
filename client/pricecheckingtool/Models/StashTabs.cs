using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    public sealed class StashTabs
    {
        public string numTabs { get; set; }
        public ObservableCollection<StashTab> tabs { get; set; }

        public StashTabs()
        {
            tabs = new ObservableCollection<StashTab>();
        }
    }
}
