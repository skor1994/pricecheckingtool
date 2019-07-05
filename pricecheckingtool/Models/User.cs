using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace pricecheckingtool
{
    class User
    {
        public string sessionID { get; private set; }
        public string accName { get; private set; }
        public List<StashTab> stashTabs = new List<StashTab>();

        public User(string sessionID, string accName)
        {
            this.sessionID = sessionID;
            this.accName = accName;

            WriteToFile();
        }

        public User()
        {
            GetDataFromFile();
        }

        private void WriteToFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "user.txt";

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();

                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine($"sessionID:{sessionID}");
                    tw.WriteLine($"accName:{accName}");
                    tw.Close();
                }
            }
        }

        private void GetDataFromFile()
        {
            StreamReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "user.txt");
            string line = string.Empty;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("sessionID"))
                    sessionID = line.Remove(0, 10);
                else if (line.Contains("accName"))
                    accName = line.Remove(0, 8);
            }
        }

        private Dictionary<string, dynamic> FetchUserStashTabs()
        {
            string link = $"www.pathofexile.com/character-window/get-stash-items/?league=legion&accountName={accName}&tabs=1";
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + link);
            request.Method = "Get";
            request.KeepAlive = true;
            request.ContentType = "appication/json";
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(GetCookie());

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                data = new JavaScriptSerializer().Deserialize<Dictionary<string, dynamic>>(sr.ReadToEnd());
            }
            response.Close();

            return data;
        }

        public void GetUserStashTabs()
        {
            stashTabs.Clear();
            Dictionary<string, dynamic> userStashTabs = FetchUserStashTabs();

            foreach(var arrayList in userStashTabs["tabs"])
            {
                string name = string.Empty;
                string type = string.Empty;
                string id = string.Empty;

                foreach (KeyValuePair<string, dynamic> keyValuePair in arrayList)
                {
                    Debug.Print(keyValuePair.Key);
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
                }

                stashTabs.Add(new StashTab(name, type, id, null));
            }
        }

        private Cookie GetCookie()
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
