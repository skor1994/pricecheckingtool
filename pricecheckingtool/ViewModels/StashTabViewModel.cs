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
        private readonly User user = UserViewModel.user;
        public ICommand command;


        


        //priceLists.GetPrices();


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
