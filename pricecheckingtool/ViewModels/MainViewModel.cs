using pricecheckingtool.Provider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Script.Serialization;
using System.Windows.Input;

namespace pricecheckingtool.ViewModels
{
    class MainViewModel: ViewModelBase
    {
        private User user;
        private Webservice webservice;
        private ICommand loadStashTabsCommand;
        private ICommand saveDataCommand;
        private ICommand loginCommand;
        private StashTab stashTab;
        private PriceLists priceLists;
        private Timer timerPriceLists;

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

        public ObservableCollection<Item> Items
        {
            get { return stashTab.items; }
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
            priceLists = new PriceLists();
            SetupTimerPriceLists();
        }

        public ICommand LoginCommand
        {
            get
            {
                return loginCommand ?? (loginCommand = new DelegateCommand(() => Login(), canExecute => (DataFileExistsAndIsNotEmpty() == true)));
            }
        }

        public ICommand SaveDataCommand
        {
            get
            {
                return saveDataCommand ?? (saveDataCommand = new DelegateCommand(() => SaveAndLogin(), canExecute => (AccountName != null && SessionID != null && League != null)));
            }
        }

        public ICommand LoadStashTabsCommand
        {
            get
            {
                return loadStashTabsCommand ?? (loadStashTabsCommand = new DelegateCommand(() => LoadStashTabs(), canExecute => user != null));
            }
        }
        
        public void Login()
        {
            user.LoadDataFromFile();
            webservice = new Webservice(user.sessionID);
            RaiseEvents();
        }

        public void SaveAndLogin()
        {
            user.CreateDataFile();
            webservice = new Webservice(user.sessionID);
            RaiseEvents();
        }

        public void RaiseEvents()
        {
            RaisePropertyChanged("AccountName");
            RaisePropertyChanged("SessionID");
            RaisePropertyChanged("League");
        }
        
        public async void LoadStashTabs()
        {
            string link = $"https://www.pathofexile.com/character-window/get-stash-items/?league={user.league}&accountName={user.accountName}&tabs=1";
            var responseString = await webservice.httpClientWithCookie.GetStringAsync(link);
            user.stashTabs = new JavaScriptSerializer().Deserialize<StashTabs>(responseString);
            RaisePropertyChanged("StashTabs");
        }

        public bool DataFileExistsAndIsNotEmpty()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "user.txt";

            if (File.Exists(path) && new FileInfo(path).Length != 0)
                return true;
            else
                return false;
        }

        private void SetupTimerPriceLists()
        {
            timerPriceLists = new Timer();
            timerPriceLists.Interval = 1800000;
            timerPriceLists.Elapsed += OnTimeElapsedPriceLists;
            timerPriceLists.Start();
        }

        private void OnTimeElapsedPriceLists(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            FetchPrices();
        }

        public async void GetItems()
        {
            string link = $"https://www.pathofexile.com/character-window/get-stash-items/?league={user.league}&accountName={user.accountName}&tabIndex={stashTab.i}";
            var responseString = await webservice.httpClientWithCookie.GetStringAsync(link);
            stashTab = new JavaScriptSerializer().Deserialize<StashTab>(responseString);

            foreach (Item item in stashTab.items)
            {
                item.SetItemName();
                item.SetStackSize();
                item.checkPrice(priceLists);
            }

            RaisePropertyChanged("Items");
        }

        public async void FetchPrices()
        {
            foreach (Category category in Enum.GetValues(typeof(Category)))
            {
                string link = $"https://api.poe.watch/get?league={user.league}&category={category}";
                var responseString = await webservice.httpClient.GetStringAsync(link);

                priceLists.prices.Add(new JavaScriptSerializer().Deserialize<List<Item>>(responseString));
            }
        }
    }
}
