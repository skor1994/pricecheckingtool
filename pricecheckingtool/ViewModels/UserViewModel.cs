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
        public readonly static User user = new User();
        private ICommand command;
        public ICommand clickCommand
        {
            get
            {
                return command ?? (command = new DelegateCommand(() => LoadData()));
            }
        }

        public string AccountName
        {
            get { return user.accountName; }
            set
            {
                user.accountName = value;
                RaisePropertyChanged("AccountName");
            }
        }
        public string SessionID
        {
            get { return user.sessionID; }
            set
            {
                user.accountName = value;
                RaisePropertyChanged("SessionID");
            }
        }
        public string League
        {
            get { return user.league; }
            set
            {
                user.accountName = value;
                RaisePropertyChanged("League");
            }
        }

        public ICommand LoadDataCommand()
        {
            return new DelegateCommand(LoadData);
        }

        public void LoadData()
        {
            user.GetDataFromFile();
        }

        public ICommand LoginCommand()
        {
            return new DelegateCommand(Login);
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
