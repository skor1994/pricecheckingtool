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
        public static User user = new User();
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

    }
}
