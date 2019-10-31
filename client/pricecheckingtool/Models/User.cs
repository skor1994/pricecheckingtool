using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace pricecheckingtool
{
    public sealed class User
    {
        public string sessionID { get; set; }
        public string name { get; set; }
        public string league { get; set; }
        public StashTabs stashTabs { get; set; }
        public int userId { get; set; }
        public ObservableCollection<Party> parties { get; set; }

        public User()
        {
            stashTabs = new StashTabs();
            parties = new ObservableCollection<Party>();
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
                    name = line.Remove(0, 8);
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
                tw.WriteLine($"accName:{name}");
                tw.WriteLine($"league:{league}");
                tw.Close();
            }
        }
    }
}
