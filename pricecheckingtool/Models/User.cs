using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pricecheckingtool
{
    class User
    {
        public string sessionID { get; private set; }
        public string accName { get; private set; }

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
    }
}
