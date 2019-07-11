using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace pricecheckingtool.ViewModels
{
    public sealed class StashTabViewModel : ViewModelBase
    {
        private readonly User user = App.user;
        private readonly List<StashTab> stashTabs = App.user.stashTabs;
        private readonly PriceLists priceLists = new PriceLists();
        public ICommand command;

        public ICommand FetchPricesCommand
        {
            get
            {
                return command ?? (command = new DelegateCommand(() => FetchPrices()));
            }
        }

        private void FetchPrices()
        {
            priceLists.GetPrices();
        }
    }
}
