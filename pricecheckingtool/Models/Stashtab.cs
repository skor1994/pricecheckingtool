using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace pricecheckingtool
{

    public sealed class StashTab
    {
        public string n { get; set; }
        public int i { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public bool hidden { get; set; }
        public bool selected { get; set; }
        public ObservableCollection<Item> items { get; set; } = new ObservableCollection<Item>();

        public StashTab()
        {

        }

        private Dictionary<string, dynamic> FetchStashInventory(Cookie cookie, string accName)
        {
            string link = $"www.pathofexile.com/character-window/get-stash-items/?league=legion&accountName={accName}&tabIndex={i}";
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + link);
            request.Method = "Get";
            request.KeepAlive = true;
            request.ContentType = "appication/json";
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(cookie);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                data = new JavaScriptSerializer().Deserialize<Dictionary<string, dynamic>>(sr.ReadToEnd());
            }
            response.Close();

            return data;
        }
        
        public void GetStashInventory(Cookie cookie, string accName)
        {
            items.Clear();
            Dictionary<string, dynamic> stashInventory = FetchStashInventory(cookie, accName);

            foreach (var arrayList in stashInventory["items"])
            {
                int itemRarity = 0;               
                string itemName = string.Empty;
                int itemLvl = 0;
                bool isIdentified = false;
                string typeLine = string.Empty;
                int stackSize = 0;
                List<string> explicitMods = new List<string>();

                foreach (KeyValuePair<string, dynamic> keyValuePair in arrayList)
                {
                    if (keyValuePair.Key == "frameType")
                    {
                        itemRarity = (int)keyValuePair.Value;
                    }
                    else if (keyValuePair.Key == "name")
                    {
                        itemName = (string)keyValuePair.Value;
                    }
                    else if (keyValuePair.Key == "ilvl")
                    {
                        itemLvl = (int)keyValuePair.Value;
                    }
                    else if (keyValuePair.Key == "identified")
                    {
                        isIdentified = (bool)keyValuePair.Value;
                    }
                    else if (keyValuePair.Key == "typeLine")
                    {
                        typeLine = (string)keyValuePair.Value;
                    }
                    else if (keyValuePair.Key == "stackSize")
                    {
                        stackSize = (int)keyValuePair.Value;
                    }
                    else if (keyValuePair.Key == "explicitMods")
                    {
                        foreach (string value in keyValuePair.Value)
                        {
                            explicitMods.Add(value);
                        }
                    }
                }
                items.Add(new Item(itemRarity, itemName, itemLvl, isIdentified, typeLine, stackSize, explicitMods));
            }
        }

    }
}
