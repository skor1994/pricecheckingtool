using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace pricecheckingtool.ViewModels
{
    public sealed class UserViewModel : ViewModelBase
    {
        private readonly User user = App.user;
        public ICommand command;

        public string AccountName
        {
            get { return user.accountName; }
            set
            {
                user.accountName = value;
                RaisePropertyChanged();
            }
        }
        public string SessionID
        {
            get { return user.sessionID; }
            set
            {
                user.sessionID = value;
                RaisePropertyChanged();
            }
        }
        public string League
        {
            get
            {
                return user.league;
            }
            set
            {
                user.league = value;
                RaisePropertyChanged();
            }
        }

        public IEnumerable StashTabs
        {
            get { return user.stashTabs; }
        }

        public ICommand LoadDataCommand
        {
            get
            {
                return command ?? (command = new DelegateCommand(() => LoadData()));
            }
        }
        public void LoadData()
        {
            user.GetDataFromFile();
            user.GetStashTabs(user.GetCookie());
            
            RaisePropertyChanged("AccountName");
            RaisePropertyChanged("SessionID");
            RaisePropertyChanged("League");
            RaisePropertyChanged("StashTabs");
        }

        public ICommand LoginCommand
        {
            get
            {
                return command ?? (command = new DelegateCommand(() => Login()));
            }
        }

        private void Login()
        {
            if (!user.HasDataFile())
            {
                user.WriteToFile();
            }
        }

        public ICommand FetchStashInventoryCommand
        {
            get
            {
                return command ?? (command = new DelegateCommand(() => FetchStashInventory()));
            }
        }

        private void FetchStashInventory()
        {
            if (!user.HasDataFile())
            {
                user.WriteToFile();
            }
        }

        public ICommand MouseClickCommand
        {
            get
            {
                return command ?? (command = new DelegateCommand(() => MouseClick()));
            }
        }

        private void MouseClick()
        {
           
        }
        //private void ListViewTabs_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    listViewItems.Items.Clear();

        //    var stashTab = (StashTab)(sender as ListView).SelectedItem;

        //    if (stashTab != null)
        //    {
        //        stashTab.GetStashInventory(GetCookie(), user.accountName);

        //        foreach (Item item in stashTab.items)
        //        {
        //            listViewItems.Items.Add(item);
        //        }
        //    }
        //}
    }
}
