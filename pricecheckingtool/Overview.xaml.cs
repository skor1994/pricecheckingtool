using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Web.Script.Serialization;

namespace pricecheckingtool
{
    /// <summary>
    /// Interaktionslogik für Overview.xaml
    /// </summary>
    public partial class Overview : Window
    {
        static User user = new User();
        static PriceLists priceLists = new PriceLists();

        public Overview()
        {
            InitializeComponent();
            InitializeStashTabView();
            priceLists.GetPrices();
        }

        private void InitializeStashTabView()
        {
            
            user.GetStashTabs(GetCookie());

            foreach (StashTab stashTab in user.stashTabs)
            {
                listViewTabs.Items.Add(stashTab);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listViewTabs.Items.Clear();
            InitializeStashTabView();
        }

        private void ListViewTabs_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            listViewItems.Items.Clear();

            var stashTab = (StashTab)(sender as ListView).SelectedItem;

            if (stashTab != null)
            {
                stashTab.GetStashInventory(GetCookie(), user.accName);

                foreach (Item item in stashTab.items)
                {
                    listViewItems.Items.Add(item);
                }
            }
        }
        public static Cookie GetCookie()
        {
            Cookie cookie = new Cookie();
            cookie.Value = user.sessionID;
            cookie.Name = "POESESSID";
            cookie.Domain = "pathofexile.com";
            cookie.Secure = false;
            cookie.Path = "/";
            cookie.HttpOnly = false;

            return cookie;
        }
    }
}
