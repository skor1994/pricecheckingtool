using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using pricecheckingtool.Provider;

namespace pricecheckingtool
{
    enum Category { weapon, prophecy, map, jewel, flask, card, currency, armour, accessory}

    public sealed class PriceLists
    {
        public List<List<Item>> prices { get; set; }

        public PriceLists()
        {
            prices = new List<List<Item>>();
        }

    }
}
