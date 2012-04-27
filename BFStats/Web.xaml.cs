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
using Microsoft.Phone.Shell;

namespace BFStats
{
    public partial class Web : PhoneApplicationPage
    {
        public Web()
        {
            InitializeComponent();
            browser.IsScriptEnabled = true;
            browser.Navigate(new Uri("http://bf3stats.com/stats_"+Configs.GetPlatform()+"/"+Configs.GetPlayer(), UriKind.Absolute));
        }

        private void browser_Navigating(object sender, NavigatingEventArgs e)
        {
            SystemTray.SetIsVisible(this, true);
            ProgressIndicator prog = new ProgressIndicator();
            prog.IsVisible = true;
            prog.IsIndeterminate = true;
            prog.Text = "Loading BF3stats.com...";
            SystemTray.SetProgressIndicator(this, prog);
        }

        private void browser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            
        }

        private void browser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                SystemTray.SetIsVisible(this, false);
                if (Configs.GetFirstLoad())
                {
                    MessageBox.Show("This will load BF3stats.com. This circumvents API restrictions.\n\nIn the top right corner, it will show you progress. When done, click back on your phone, then 'Pull'");
                    Configs.SetFirstLoad(false);
                    //browser.InvokeScript("eval", "setTimeout(player.updatePlayer(), 1000)");
                    //browser.InvokeScript("player.updatePlayer");
                    browser.InvokeScript("eval", "player.updatePlayer();");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}