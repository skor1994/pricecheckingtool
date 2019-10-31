using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace pricecheckingtool.ViewModels
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> action;
        private readonly Predicate<object> canExecute;

        public event EventHandler CanExecuteChangedInternal;


        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                this.CanExecuteChangedInternal -= value;
            }
        }

        public DelegateCommand(Action<object> action)
        {
            this.action = action;
        }

        public DelegateCommand(Action<object> action, Predicate<object> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute != null ? canExecute(parameter) : true;
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }
        public void OnCanExecuteChanged()
        {
            CanExecuteChangedInternal.Invoke(this, EventArgs.Empty);
        }
    }
}
