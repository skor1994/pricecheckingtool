using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    public sealed class Party
    {
        public string name { get; set; }
        public int partyId { get; set; }
        public ObservableCollection<User> users { get; set; }

        public Party()
        {

        }
    }
}
