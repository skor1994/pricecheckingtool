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
        public double median { get; set; }
        public double singleValue { get; set; }
        public double value { get; set; }
        public string itemName { get; set; }
        public bool identified { get; set; }
        public int ilvl { get; set; }
        public int stackSize { get; set; }
        public string typeLine { get; set; }
        public int frameType { get; set; }
        public string id { get; set; }

        public void checkPrice(PriceLists priceLists)
        {
            foreach(List<Item> item in priceLists.prices)
            {
                if (frameType != 3)
                {
                    int index = item.FindIndex(i => i.name == typeLine);

                    if (index > 0 && stackSize > 0)
                    {
                        mean = Math.Round(item.ElementAt(index).mean, 1);
                        median = Math.Round(item.ElementAt(index).median, 1);
                        singleValue = (mean + median) / 2;
                        value = stackSize * singleValue;
                    }
                    else if (index > 0)
                    {
                        mean = Math.Round(item.ElementAt(index).mean, 1);
                        median = Math.Round(item.ElementAt(index).median, 1);
                        singleValue = (mean + median) / 2;
                        value = singleValue;
                    }                   
                    else
                        continue;
                }
                else
                {
                    int index = item.FindIndex(i => i.name == name);
                    if (index > 0)
                    {
                        mean = Math.Round(item.ElementAt(index).mean, 1);
                        median = Math.Round(item.ElementAt(index).median, 1);
                        singleValue = (mean + median) / 2;
                        value = singleValue;
                    }      
                    else
                        continue;
                }
            }
        }
        public void SetItemName()
        {
            if(name == null || name == "")
            {
                itemName = typeLine;
            }
            else
            {
                itemName = name;
            }
        }
        public void SetStackSize()
        {
            if (stackSize == 0)
            {
                stackSize = 1;
            }
        }
    }
}
