using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace BFStats
{
    public partial class Settings : PhoneApplicationPage
    {
        private RadioButton rbselected = null;
        public Settings()
        {
            InitializeComponent();
        }

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            if ((txt_player.Text.ToString() == "") || (rbselected == null))
            {
                MessageBox.Show("You must enter a gamertag and a platform");
            }
            else
            {
                Configs.SetPlayer(txt_player.Text.ToString());
                Configs.SetPlatform(rbselected.Name.ToString());
                Configs.SetPlayerJSON("");
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

        private void rb_Click(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            rbselected = rb;
        }
    }
}