using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace pricecheckingtool
{
    enum Category { weapon, prophecy, map, jewel, flask, card, currency, armour, accessory, gem}

    public sealed class PriceLists
    {
        private static readonly HttpClient client = new HttpClient();

        public static List<Item> weapon = new List<Item>();
        public static List<Item> prophecy = new List<Item>();
        public static List<Item> map = new List<Item>();
        public static List<Item> jewel = new List<Item>();
        public static List<Item> flask = new List<Item>();
        public static List<Item> card = new List<Item>();
        public static List<Item> currency = new List<Item>();
        public static List<Item> armour = new List<Item>();
        public static List<Item> accessory = new List<Item>();


        public async void GetPrices()
        {
            foreach (Category category in Enum.GetValues(typeof(Category)))
            {
                string link = $"https://api.poe.watch/get?league=Legion&category={category}";
                var responseString = await client.GetStringAsync(link);

                switch (category)
                {
                    case Category.weapon: weapon = new JavaScriptSerializer().Deserialize<List<Item>>(responseString); break;
                    case Category.prophecy: prophecy = new JavaScriptSerializer().Deserialize<List<Item>>(responseString); break;
                    case Category.map: map = new JavaScriptSerializer().Deserialize<List<Item>>(responseString); break;
                    case Category.jewel: jewel = new JavaScriptSerializer().Deserialize<List<Item>>(responseString); break;
                    case Category.flask: flask = new JavaScriptSerializer().Deserialize<List<Item>>(responseString); break;
                    case Category.card: card = new JavaScriptSerializer().Deserialize<List<Item>>(responseString); break;
                    case Category.currency: currency = new JavaScriptSerializer().Deserialize<List<Item>>(responseString); break;
                    case Category.armour: armour = new JavaScriptSerializer().Deserialize<List<Item>>(responseString); break;
                    case Category.accessory: accessory = new JavaScriptSerializer().Deserialize<List<Item>>(responseString); break;
                }              
            }
        }
    }
}
