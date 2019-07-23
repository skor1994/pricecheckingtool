using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace pricecheckingtool
{
    enum Category { weapon, prophecy, map, jewel, flask, card, currency, armour, accessory}

    public sealed class PriceLists
    {
        private static readonly HttpClient client = new HttpClient();

        public static List<List<Item>> prices = new List<List<Item>>();

        public async void GetPrices()
        {
            foreach (Category category in Enum.GetValues(typeof(Category)))
            {
                string link = $"https://api.poe.watch/get?league=Legion&category={category}";
                var responseString = await client.GetStringAsync(link);

                prices.Add(new JavaScriptSerializer().Deserialize<List<Item>>(responseString));          
            }
        }
    }
}
