using pricecheckingtool.Provider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Data;
using System.Windows.Input;

namespace pricecheckingtool.ViewModels
{
    class MainViewModel: ViewModelBase
    {
        private User user;
        private Webservice webservice;
        private ICommand saveDataCommand;
        private ICommand loginCommand;
        private ICommand sortCommand;
        private StashTab stashTab;
        private PriceLists priceLists;
        private string sortColumn;
        private ListSortDirection listSortDirection;
        private ListCollectionView stashItemsCollection;

        public string AccountName
        {
            get { return user.accountName; }
            set { user.accountName = value; RaisePropertyChanged(); }
        }

        public string SessionID
        {
            get { return user.sessionID; }
            set { user.sessionID = value; RaisePropertyChanged(); }
        }

        public string League
        {
            get { return user.league; }
            set { user.league = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<StashTab> StashTabs
        {
            get { return user.stashTabs.tabs; }
        }

        public ListCollectionView Items
        {
            get { return stashItemsCollection; }
            set { stashItemsCollection = value; RaisePropertyChanged(); }
        }

        public StashTab selectedStashTab
        {
            get { return stashTab; }
            set { stashTab = value; GetItems(); RaisePropertyChanged(); }
        }

        public MainViewModel()
        {
            user = new User();
            stashTab = new StashTab();
        }

        public ICommand LoginCommand
        {
            get
            {
                return loginCommand ?? (loginCommand = new DelegateCommand((param) => Login(), canExecute => (DataFileExistsAndIsNotEmpty() == true)));
            }
        }

        public ICommand SaveDataCommand
        {
            get
            {
                return saveDataCommand ?? (saveDataCommand = new DelegateCommand((param) => SaveAndLogin(), canExecute => (AccountName != null && SessionID != null && League != null)));
            }
        }

        public ICommand SortCommand
        {
            get
            {
                return sortCommand ?? (sortCommand = new DelegateCommand((param) => Sort(param), canExecute => stashItemsCollection != null));
            }
        }

        private void Login()
        {
            user.LoadDataFromFile();
            webservice = new Webservice(user.sessionID);
            RaiseEvents("");
            FetchData();
        }

        private void SaveAndLogin()
        {
            user.CreateDataFile();
            Login();
        }

        private async void FetchData()
        {
            priceLists = await FetchPriceListsData();
            user.stashTabs = await FetchStashTabsData();
            RaiseEvents("StashTabs");      
        }

        private void Sort(object parameter)
        {
            string column = parameter as string;

            if (sortColumn == column)
            {
                listSortDirection = listSortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            }
            else
            {
                sortColumn = column;
                listSortDirection = ListSortDirection.Ascending;
            }

            stashItemsCollection.SortDescriptions.Clear();
            stashItemsCollection.SortDescriptions.Add(new SortDescription(sortColumn, listSortDirection));

            RaiseEvents("Items");
        }

        private async Task<StashTabs> FetchStashTabsData()
        {
            string link = $"https://www.pathofexile.com/character-window/get-stash-items/?league={user.league}&accountName={user.accountName}&tabs=1";

            try
            {
                var responseString = await webservice.httpClientWithCookie.GetStringAsync(link);
                return new JavaScriptSerializer().Deserialize<StashTabs>(responseString); 
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }

        private async Task<PriceLists> FetchPriceListsData()
        {
            PriceLists priceLists = new PriceLists();

            foreach (Category category in Enum.GetValues(typeof(Category)))
            {
                string link = $"https://api.poe.watch/get?league={user.league}&category={category}";

                try
                {
                    var responseString = await webservice.httpClient.GetStringAsync(link);
                    priceLists.prices.Add(new JavaScriptSerializer().Deserialize<List<Item>>(responseString));
                }
                catch (HttpRequestException e)
                {
                    throw e;
                }
            }

            return priceLists;
        }

        private async void GetItems()
        {

            StashTab stashTab = await FetchStashItems();

            foreach (Item item in stashTab.items)
            {
                item.NormalizeItemName();
                item.NormalizeStackSize();
                item.checkPrice(priceLists);
            }

            stashItemsCollection = new ListCollectionView(stashTab.items);
            RaiseEvents("Items");
        }

        private async Task<StashTab> FetchStashItems()
        {
            string link = $"https://www.pathofexile.com/character-window/get-stash-items/?league={user.league}&accountName={user.accountName}&tabIndex={stashTab.i}";

            try
            {
                var responseString = await webservice.httpClientWithCookie.GetStringAsync(link);
                return new JavaScriptSerializer().Deserialize<StashTab>(responseString);
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }

        private void RaiseEvents(string type)
        {
            switch (type)
            {
                case "StashTabs":
                    RaisePropertyChanged("StashTabs");
                    break;

                case "Items":
                    RaisePropertyChanged("Items");
                    break;

                default:
                    RaisePropertyChanged("AccountName");
                    RaisePropertyChanged("SessionID");
                    RaisePropertyChanged("League");
                    break;
            }
        }

        private bool DataFileExistsAndIsNotEmpty()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "user.txt";

            if (File.Exists(path) && new FileInfo(path).Length != 0)
                return true;
            else
                return false;
        }
    }
}
