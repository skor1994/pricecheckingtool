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
        public string sessionID { get; }
        public string accName { get; }

        public User(string sessionID, string accName)
        {
            this.sessionID = sessionID;
            this.accName = accName;

            WriteToFile();
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
    }
}
