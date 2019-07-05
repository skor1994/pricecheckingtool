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
            InitializeStashTabView();
        }

        private void InitializeStashTabView()
        {
            List<StashTab> stashTabs = new List<StashTab>();
            List<Item> items = new List<Item>();
            List<Item> items2 = new List<Item>();
            List<Item> items3 = new List<Item>();

            items.Add(new Amulet("testamulet", ItemRarity.rare, ItemBase.shaper, false, 81, null, "5c"));
            items.Add(new Amulet("ring", ItemRarity.normal, ItemBase.normal, false, 81, null, "3alch"));

            items2.Add(new Amulet("testamulet2", ItemRarity.rare, ItemBase.shaper, false, 81, null, "5c"));
            items2.Add(new Amulet("ring2", ItemRarity.normal, ItemBase.normal, false, 81, null, "3alch"));

            items3.Add(new Amulet("testamulet3", ItemRarity.rare, ItemBase.shaper, false, 81, null, "5c"));
            items3.Add(new Amulet("ring3", ItemRarity.normal, ItemBase.normal, false, 81, null, "3alch"));

            stashTabs.Add(new QuadTab("Dump", items));
            stashTabs.Add(new PremiumTab("Rings", items2));
            stashTabs.Add(new NormalTab("2", items3));

            foreach (StashTab stashTab in stashTabs)
            {
                this.listViewTabs.Items.Add(stashTab);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> userData = UserData();
            string link = $"www.pathofexile.com/character-window/get-stash-items/?league=legion&accountName={userData.ElementAt(1)}&tabIndex=1&tabs=1";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + link);
            request.Method = "Get";
            request.KeepAlive = true;
            request.ContentType = "appication/json";
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(CreateCookie(userData.ElementAt(0)));

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string myResponse = "";
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                myResponse = sr.ReadToEnd();
            }
            response.Close();
        }

        private List<string> UserData()
        {
            StreamReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "user.txt");
            string line = string.Empty;
            List<string> userData = new List<string>();

            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("sessionID"))
                    userData.Add(line.Remove(0, 10));
                else if (line.Contains("accName"))
                    userData.Add(line.Remove(0, 8));
            }
            return userData;
        }

        private Cookie CreateCookie(string sessionID)
        {
            Cookie cookie = new Cookie();
            cookie.Value = sessionID;
            cookie.Name = "POESESSID";
            cookie.Domain = "pathofexile.com";
            cookie.Secure = false;
            cookie.Path = "/";
            cookie.HttpOnly = false;

            return cookie;
        }

        private void ListViewTabs_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.listViewItems.Items.Clear();

            var stashTab = (StashTab)(sender as ListView).SelectedItem;

            if (stashTab != null)
            {
                foreach (Item item in stashTab.items)
                {
                    this.listViewItems.Items.Add(item);
                }
            }
        }
    }
}
