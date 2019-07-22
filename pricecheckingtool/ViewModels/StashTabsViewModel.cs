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
    class StashTabsViewModel : ViewModelBase
    {
        private readonly User user = App.user;
        private StashTabs stashTabs = new StashTabs();
        private StashTab stashTab = new StashTab();
        public ICommand command;

        public ObservableCollection<StashTab> StashTabs
        {
            get { return stashTabs.tabs; }
        }

        public StashTab selectedStashTab
        {
            get { return stashTab; }
            set
            {
                stashTab = value;
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

            string link = $"https://www.pathofexile.com/character-window/get-stash-items/?league=legion&accountName={user._accountName}&tabs=1";
            var responseString = await httpClient.GetStringAsync(link);
            stashTabs = new JavaScriptSerializer().Deserialize<StashTabs>(responseString);

            RaisePropertyChanged("StashTabs");
        }
    }
}
