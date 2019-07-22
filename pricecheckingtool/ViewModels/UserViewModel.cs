using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Item> items { get; set; }
        public ICommand command;

        public UserViewModel()
        {
            items = new ObservableCollection<Item>();
        }

        public string AccountName
        {
            get { return user._accountName; }
            set
            {
                user._accountName = value;
                RaisePropertyChanged();
            }
        }
        public string SessionID
        {
            get { return user._sessionID; }
            set
            {
                user._sessionID = value;
                RaisePropertyChanged();
            }
        }
        public string League
        {
            get
            {
                return user._league;
            }
            set
            {
                user._league = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Item> Items
        {
            get { return items; }
            set
            {
                items = value;
                RaisePropertyChanged();
            }
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
            
            RaisePropertyChanged("AccountName");
            RaisePropertyChanged("SessionID");
            RaisePropertyChanged("League");
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
