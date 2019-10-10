using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace pricecheckingtool
{
    public sealed class User
    {
        public string sessionID { get; set; }
        public string accountName { get; set; }
        public string league { get; set; }
        public StashTabs stashTabs { get; set; }

        public User()
        {
            stashTabs = new StashTabs();
        }

        public void LoadDataFromFile()
        {
            StreamReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "user.txt");
            string line;
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

        public void CreateDataFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "user.txt";

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
}
