using System;
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
    public sealed class Item 
    {
        public string name { get; set; }
        public double mean { get; set; }
        public bool identified { get; set; }
        List<string> explicitMods { get; set; }
        List<string> implicitMods { get; set; }
        public int ilvl { get; set; }
        public int stackSize { get; set; }
        public string typeLine { get; set; }
        public int frameType { get; set; }
        public string id { get; set; }

        public Item()
        {

        }

        public void checkPrice()
        {
            foreach(List<Item> item in PriceLists.prices)
            {
                if (frameType != 3)
                {
                    int index = item.FindIndex(i => i.name == typeLine);

                    if (index > 0 && stackSize > 0)
                        mean = stackSize * Math.Round(item.ElementAt(index).mean, 1);
                    else if (index > 0)
                        mean = Math.Round(item.ElementAt(index).mean, 1);
                    else
                        continue;
                }
                else
                {
                    int index = item.FindIndex(i => i.name == name);
                    if (index > 0)
                        mean = Math.Round(item.ElementAt(index).mean, 1);
                    else
                        continue;
                }
            }
        }
    }
}
