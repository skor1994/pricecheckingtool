using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace pricecheckingtool
{
    /// <summary>
    /// Interaktionslogik für Overview.xaml
    /// </summary>
    public partial class Overview : Window
    {
        public Overview()
        {
            InitializeComponent();

            // testing the listView for Tabs
            List<StashTab> stashTabs = new List<StashTab>();

            stashTabs.Add(new QuadTab("Dump"));
            stashTabs.Add(new PremiumTab("Rings"));
            stashTabs.Add(new NormalTab("2"));

            foreach (StashTab stashTab in stashTabs)
            {
                this.listViewTabs.Items.Add(stashTab);
            }

            // testing the listView for Items
            List<Item> items = new List<Item>();

            for (int i = 0; i < 3; i++)
            {
                items.Add(new Amulet("testamulet", ItemRarity.rare, ItemBase.shaper, false, 81, null, "5c"));
                items.Add(new Amulet("ring", ItemRarity.normal, ItemBase.normal, false, 81, null, "3alch"));
                items.Add(new Amulet("mark of", ItemRarity.unique, ItemBase.elder, false, 81, null, "1ex"));
                items.Add(new Ring("testring", ItemRarity.rare, ItemBase.shaper, false, 81,null, null, "0.7ex"));
            }

            foreach(Item item in items)
            {
                this.listViewItems.Items.Add(item);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // value = sessionid

            Cookie cookie = new Cookie();
            cookie.Value = "";
            cookie.Name = "POESESSID";
            cookie.Domain = "pathofexile.com";
            cookie.Secure = false;
            cookie.Path = "/";
            cookie.HttpOnly = false;

            // link to www.pathofexile.com/character-window/get-stash-items/?league={}&accountName={}&tabIndex={}&tabs={}

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("");
            request.Method = "Get";
            request.KeepAlive = true;
            request.ContentType = "appication/json";
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(cookie);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string myResponse = "";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
            {
                myResponse = sr.ReadToEnd();
                Debug.Print(myResponse);
            }
            response.Close();
        }
    }
}
