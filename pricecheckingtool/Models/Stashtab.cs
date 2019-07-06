using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace pricecheckingtool
{

    class StashTab
    {
        public string name { get; }
        public string type { get; }
        public string id { get; }
        public int number { get; set; }
        public List<Item> items = new List<Item>();

        public StashTab(string name, string type, string id, int number, List<Item> items)
        {
            this.name = name;
            this.type = type;
            this.id = id;
            this.items = items;
            this.number = number;
        }

        private Dictionary<string, dynamic> FetchStashInventory(Cookie cookie, string accName)
        {
            string link = $"www.pathofexile.com/character-window/get-stash-items/?league=legion&accountName={accName}&tabIndex={number}";
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

        public void GetStashInventory()
        {

        }

    }
}
