using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace pricecheckingtool
{
    enum ItemRarity { normal, magic, rare, unique, gem, currency, divination, quest, prophecy, relic };
    enum ItemBaseType { OneHandedWeapon, TwoHandedWeapon, Jewel, Ring, Amulet, Belt, Gloves, Boots, BodyArmour, Helmet, Shield, Quiver };
    enum ItemBase { shaper, elder, normal };

    class Item
    {
        public string name { get; }
        public ItemRarity itemRarity { get; }
        public ItemBaseType itemBaseType { get; }
        public ItemBase itemBase { get; }
        public bool isIdentified { get; }
        List<string> explicitMods { get; }
        public string[] mods { get; }
        public int itemlevel { get; }
        public Dictionary<char, int> socketsAndColors { get; }
        public int links { get; }
        public int stackSize { get; }
        public string typeLine { get; }
        public string value { get; }

        public Item(string name, ItemRarity itemRarity, ItemBaseType itemBaseType, ItemBase itemBase, bool isIdentified, int itemlevel, Dictionary<char, int> socketsAndColors, int links, string[] mods, string value)
        {
            this.name = name;
            this.itemRarity = itemRarity;
            this.itemBaseType = itemBaseType;
            this.itemBase = itemBase;
            this.isIdentified = isIdentified;
            this.itemlevel = itemlevel;
            this.socketsAndColors = socketsAndColors;
            this.links = links;
            this.mods = mods;
            this.value = value;
        }
        public Item(int itemRarity, string itemName, int itemLvl, bool identified, string typeLine, int stackSize, List<string> explicitMods)
        {
            this.itemRarity = (ItemRarity)itemRarity;
            this.typeLine = typeLine;
            this.stackSize = stackSize;
            this.explicitMods = explicitMods;
            name = itemName;
            itemlevel = itemLvl;
            isIdentified = identified;
        }
        public void FetchItemPriceFromSite()
        {
            string link = $"www.poeprices.info/api";
            string data = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + link);
            request.Method = "Get";
            request.KeepAlive = true;
            request.ContentType = "appication/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                data = new JavaScriptSerializer().Deserialize<string>(sr.ReadToEnd());
            }
            response.Close();
        }
    }
}
