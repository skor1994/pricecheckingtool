using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace pricecheckingtool
{
    /// <summary>
    /// Interaktionslogik für Overview.xaml
    /// </summary>
    public partial class Overview : Window
    {
        public Overview()
        {
            InitializeComponent();

            // testing the listView for Tabs

            for(int i = 0; i < 50; i++)
            {
                this.listViewTabs.Items.Add(i);
            }

            // testing the listView for Items
            List<Item> items = new List<Item>();

            for (int i = 0; i < 3; i++)
            {
                items.Add(new Amulet("testamulet", ItemRarity.rare, ItemBase.shaper, false, 81, null, "5c"));
                items.Add(new Amulet("ring", ItemRarity.normal, ItemBase.normal, false, 81, null, "3alch"));
                items.Add(new Amulet("mark of", ItemRarity.unique, ItemBase.elder, false, 81, null, "1ex"));
                items.Add(new Ring("testring", ItemRarity.rare, ItemBase.shaper, false, 81,null, null, "0.7ex"));
            }

            foreach(Item item in items)
            {
                this.listViewItems.Items.Add(item);
            }

        }
    }
}
