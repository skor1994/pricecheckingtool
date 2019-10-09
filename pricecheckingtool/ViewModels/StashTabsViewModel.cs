using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Input;

namespace pricecheckingtool.ViewModels
{
    public class StashTabsViewModel : ViewModelBase
    {
        private readonly User user = App.user;
        private readonly PriceLists priceLists = new PriceLists();
        public StashTab stashInventory = new StashTab();
        private StashTabs stashTabs = new StashTabs();
        public StashTab stashTab = new StashTab();
        public ICommand command;

        public StashTabsViewModel()
        {
            priceLists.GetPrices();
        }

        public ObservableCollection<StashTab> StashTabs
        {
            get { return stashTabs.tabs; }
        }

        public ObservableCollection<Item> Items
        {
            get { return stashInventory.items; }
        }

        public StashTab selectedStashTab
        {
            get { return stashTab; }
            set
            {
                stashTab = value;
                GetItems();
                RaisePropertyChanged();
            }
        }

        public ICommand FetchStashTabs
        {
            get
            {
                return command ?? (command = new DelegateCommand(() => GetStashTabs()));
            }
        }

        public async void GetStashTabs()
        {
            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.Add(user.GetCookie());
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.CookieContainer = cookieContainer;
            HttpClient httpClient = new HttpClient(httpClientHandler);

            string link = $"https://www.pathofexile.com/character-window/get-stash-items/?league={user._league}&accountName={user._accountName}&tabs=1";
            var responseString = await httpClient.GetStringAsync(link);
            stashTabs = new JavaScriptSerializer().Deserialize<StashTabs>(responseString);

            RaisePropertyChanged("StashTabs");
        }

        public async void GetItems()
        {
            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.Add(user.GetCookie());
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.CookieContainer = cookieContainer;
            HttpClient httpClient = new HttpClient(httpClientHandler);

            string link = $"https://www.pathofexile.com/character-window/get-stash-items/?league={user._league}&accountName={user._accountName}&tabIndex={stashTab.i}";
            var responseString = await httpClient.GetStringAsync(link);
            stashInventory = new JavaScriptSerializer().Deserialize<StashTab>(responseString);

            foreach(Item item in stashInventory.items)
            {
                item.SetItemName();
                item.SetStackSize();
                item.checkPrice();
            }

            RaisePropertyChanged("Items");
        }
    }
}
