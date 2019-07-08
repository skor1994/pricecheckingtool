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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;

namespace pricecheckingtool
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            SkipToOverview();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            User user = new User(sessionID.Text, accName.Text);

            Overview win = new Overview();
            win.Show();
            this.Close();
        }
        
        private bool IsLoggedIn()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "user.txt";

            if (File.Exists(path))
                return true;
            else
                return false;
        }

        private void SkipToOverview()
        {
            if (IsLoggedIn())
            {
                Overview win = new Overview();
                win.Show();
                this.Close();
            }
        }

    }
}
