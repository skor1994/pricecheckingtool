using System;
using System.IO;
using System.Net;

namespace pricecheckingtool
{
    public sealed class User
    {
        public string _sessionID { get; set; }
        public string _accountName { get; set; }
        public string _league { get; set; }
        public StashTabs stashTabs { get; set; } = new StashTabs();

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
                    tw.WriteLine($"sessionID:{_sessionID}");
                    tw.WriteLine($"accName:{_accountName}");
                    tw.WriteLine($"league:{_league}");
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
                    _sessionID = line.Remove(0, 10);
                if (line.Contains("accName"))
                    _accountName = line.Remove(0, 8);
                if (line.Contains("league"))
                    _league = line.Remove(0, 7);
            }
        }
       
        public Cookie GetCookie()
        {
            Cookie cookie = new Cookie();
            cookie.Value = _sessionID;
            cookie.Name = "POESESSID";
            cookie.Domain = "pathofexile.com";
            cookie.Secure = false;
            cookie.Path = "/";
            cookie.HttpOnly = false;

            return cookie;
        }
    }
}
