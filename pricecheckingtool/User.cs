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
        public string sessionID { get; set; }
        public string accName { get; set; }

        public User(string sessionID, string accName)
        {
            this.sessionID = sessionID;
            this.accName = accName;

            WriteToFile();
        }

        private void WriteToFile()
        {
        
            string path = @"C:\Users\Philipp\Documents\pricecheckingtool\pricecheckingtool\user.txt";

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();

                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine($"sessionID: {sessionID}, accName: {accName}");
                    tw.Close();
                }
            }
        }
    }
}
