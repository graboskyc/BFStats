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
using Microsoft.Phone.Tasks;

namespace BFStats
{
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();
            txt_about.Text = "This app has been created to view your Battlefield 3 Stats on your Windows Phone 7 Device.\n\nIf you have any issues, please use the support link below. Also, please rate the app if you can.\n\nThis program is not affiliated with BF3stats.com, EA, or Dice.";

            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime dtchecked = origin.AddSeconds(Convert.ToDouble(App.ViewModel.BuiltPlayer.IntDateChecked));
            DateTime dtupdated = origin.AddSeconds(Convert.ToDouble(App.ViewModel.BuiltPlayer.IntDateUpdated));

            txt_Checked.Text = dtchecked.ToShortDateString() + " " + dtchecked.ToLongTimeString();
            txt_Updated.Text = dtupdated.ToShortDateString() + " " + dtupdated.ToLongTimeString();

            txt_ident.Text = Configs.GetIdent();
            txt_key.Text = Configs.GetAPIkey();
            txt_rot.Text = Configs.GetScheduleRotation().ToString(); ;
        }

        private void btn_rate_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();

            marketplaceReviewTask.Show();
        }

        private void btn_share_Click(object sender, RoutedEventArgs e)
        {
            //http://www.windowsphone.com/en-US/apps/79203e3d-721e-4f54-bb5c-4d4ecce90cba
            ShareLinkTask shareLinkTask = new ShareLinkTask();

            shareLinkTask.Title = "BFStats for WP7";
            shareLinkTask.LinkUri = new Uri("http://www.windowsphone.com/en-US/apps/79203e3d-721e-4f54-bb5c-4d4ecce90cba", UriKind.Absolute);
            shareLinkTask.Message = "Here is a great app to manage your Battlefield 3 Stats on your Windows Phone device!";

            shareLinkTask.Show();
        }

        private void btn_support_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = "BFStats Support";
            emailComposeTask.Body = "I am having the following issue with BFStats:";
            emailComposeTask.To = "bfstats@grabosky.net";

            emailComposeTask.Show();
        }
    }
}