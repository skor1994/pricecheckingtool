using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace pricecheckingtool
{
    public sealed class User
    {
        public string sessionID { get; set; }
        public string accountName { get; set; }
        public string league { get; set; }
        public List<StashTab> stashTabs { get; set; } = new List<StashTab>();

        public User(string sessionID, string accountName, string league)
        {
            this.sessionID = sessionID;
            this.accountName = accountName;
            this.league = league;
        }
        public User()
        {

        }
        public void WriteToFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "user.txt";

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();

                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine($"sessionID:{sessionID}");
                    tw.WriteLine($"accName:{accountName}");
                    tw.WriteLine($"league:{league}");
                    tw.Close();
                }
            }
        }

        public bool HasDataFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "user.txt";

            if (File.Exists(path))
                return true;
            else
                return false;
        }

        public void GetDataFromFile()
        {
            StreamReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "user.txt");
            string line = string.Empty;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("sessionID"))
                    sessionID = line.Remove(0, 10);
                if (line.Contains("accName"))
                    accountName = line.Remove(0, 8);
                if (line.Contains("league"))
                    league = line.Remove(0, 7);
            }
        }

        private Dictionary<string, dynamic> FetchUserStashTabs(Cookie cookie)
        {
            string link = $"www.pathofexile.com/character-window/get-stash-items/?league=legion&accountName={accountName}&tabs=1";
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
        
        public void GetStashTabs(Cookie cookie)
        {
            stashTabs.Clear();
            Dictionary<string, dynamic> userStashTabs = FetchUserStashTabs(cookie);

            foreach(var arrayList in userStashTabs["tabs"])
            {
                string name = string.Empty;
                string type = string.Empty;
                string id = string.Empty;
                int number = 0;

                foreach (KeyValuePair<string, dynamic> keyValuePair in arrayList)
                {
                    if(keyValuePair.Key == "n")
                    {
                        name = keyValuePair.Value;
                    }
                    else if(keyValuePair.Key == "type")
                    {
                        type = keyValuePair.Value;
                    }
                    else if (keyValuePair.Key == "id")
                    {
                        id = keyValuePair.Value;
                    }
                    else if (keyValuePair.Key == "i")
                    {
                        number = (int) keyValuePair.Value;
                    }
                }

                stashTabs.Add(new StashTab(name, type, id, number));
            }
        }
        public Cookie GetCookie()
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
    }
}
