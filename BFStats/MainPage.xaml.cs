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
using System.IO;
using Microsoft.Phone.Shell;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Phone.Tasks;

namespace BFStats
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            Configs.SetFirstLoad(true);

            PageHandler.ClearViewModelData();

            if (Configs.HasPlayerObject())
            {
                //MessageBox.Show("building from storage");
                parseJSON(Configs.GetPlayerJSON());
            }
            else
            {
                //MessageBox.Show("getting from web");
                
                GetAPIData();
            }

            if (Configs.GetAPIkey() == "")
            {
                SignedRequest sr = new SignedRequest();
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                string unixtime = ts.TotalSeconds.ToString();
                sr.ident = Configs.Ident;
                sr.time = unixtime;
                sr.clientident = null;
                sr.name = null;
                string json = JsonConvert.SerializeObject(sr);
                sr.MakeRequest("setupkey", "global", json);
            }

            BGTaks.AddBGTaks();
        }

        private void ApplicationBarIconButton_Click_Settings(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }


        private void ApplicationBarIconButton_Click_Refresh(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Web.xaml", UriKind.Relative));
        }

        private void weaponscol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count == 1)
            {
                ItemViewModel selectedItem = (ItemViewModel)e.AddedItems[0];
                weaponscol.SelectedIndex = -1;
                string uri = "/WeaponDetails.xaml?WeaponID=" + selectedItem.Tag.ToString();
                NavigationService.Navigate(new Uri(uri, UriKind.Relative));
            }
        }

        private void playercol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count == 1)
            {
                ItemViewModel selectedItem = (ItemViewModel)e.AddedItems[0];
                playerlist.SelectedIndex = -1;
                //if (selectedItem.Tag == "Rank")
                //{
                    string uri = "/PlayerDetails.xaml";
                    NavigationService.Navigate(new Uri(uri, UriKind.Relative));
                //}
            }
        }

        private void vehicletypescol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count == 1)
            {
                ItemViewModel selectedItem = (ItemViewModel)e.AddedItems[0];
                vehicletypescol.SelectedIndex = -1;
                string uri = "/VehicleTypeDetails.xaml?VehicleTypeID=" + selectedItem.Tag.ToString();
                NavigationService.Navigate(new Uri(uri, UriKind.Relative));
            }
        }

        private void ribbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count == 1)
            {
                ItemViewModel selectedItem = (ItemViewModel)e.AddedItems[0];
                ribboncol.SelectedIndex = -1;
                MessageBox.Show(selectedItem.Tag);
            }
        }

        private void medal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count == 1)
            {
                ItemViewModel selectedItem = (ItemViewModel)e.AddedItems[0];
                medalscol.SelectedIndex = -1;
                MessageBox.Show(selectedItem.Tag);
            }
        }

        private void ApplicationBarIconButton_Click_Download(object sender, EventArgs e)
        {
            SystemTray.SetIsVisible(this, true);
            ProgressIndicator prog = new ProgressIndicator();
            prog.IsVisible = true;
            prog.IsIndeterminate = true;
            prog.Text = "Loading data from BF3stats.com...";
            SystemTray.SetProgressIndicator(this, prog);

            PageHandler.ClearViewModelData();
            GetAPIData();
            PageHandler.SetTile();

            SystemTray.SetIsVisible(this, false);
        }

        private void vehi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count == 1)
            {
                ItemViewModel selectedItem = (ItemViewModel)e.AddedItems[0];
                vehiclescol.SelectedIndex = -1;
                MessageBox.Show(selectedItem.Tag);
            }
        }

        private void equip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count == 1)
            {
                ItemViewModel selectedItem = (ItemViewModel)e.AddedItems[0];
                equipcol.SelectedIndex = -1;
                MessageBox.Show(selectedItem.Tag);
            }
        }

        private void ApplicationBarMenuItem_Click_Rate(object sender, EventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        private void ApplicationBarMenuItem_Click_About(object sender, EventArgs e)
        {
            string uri = "/About.xaml";
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }

        public void GetAPIData(bool frontEndCall = true)
        {
            try
            {
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    SystemTray.SetProgressIndicator(App.progInd, App.progInd);

                    string uri = "http://api.bf3stats.com/" + Configs.GetPlatform() + "/playerlist/";
                    //string uri = "http://10.253.47.251/dev/jsondummy.php";
                    string query = "players=" + Configs.GetPlayer() + "&opt=all";
                    WebClient wc = new WebClient();
                    wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";
                    wc.UploadStringCompleted += new UploadStringCompletedEventHandler(uploadstringcomplete);
                    wc.UploadStringAsync(new Uri(uri), "POST", query);
                }
                else
                {
                    MessageBox.Show("An internet connection is required for this application.");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("error in main: " + ex.ToString());
                Configs.ClearSettings();
            }
        }

        public void parseJSON(string json, bool frontEndCall = true)
        {
            //MessageBox.Show(json);
            try
            {
                JObject data = JObject.Parse(json);
                if ((data["status"].ToString() == "ok") && (data["failed"].ToString() == ""))
                {
                    Configs.SetPlayerJSON(json);
                    if (frontEndCall)
                    {
                        json = Configs.GetPlayerJSON();
                        Player p = new Player(json);
                        PageHandler.BuildPage(p);
                    }
                    SystemTray.SetIsVisible(App.progInd, false);
                }
                else
                {
                    MessageBox.Show("Data from BF3Stats.com was not complete. If this is your first use, click refresh button below.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("error in json parse: " + ex.ToString());
                MessageBox.Show("JSON: " + json);
                Configs.ClearSettings();
            }
        }

        void uploadstringcomplete(Object sender, UploadStringCompletedEventArgs e)
        {
            string json = (string)e.Result;
            if (json.Length < 5)
            {
                MessageBox.Show("BF3Stats.com is down. Cannot download your statistics. Try back later.");
            }
            else
            {
                parseJSON(json);
            }
        }

        private void ApplicationBarMenuItem_Click_api(object sender, EventArgs e)
        {
            SignedRequest sr = new SignedRequest();
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            string unixtime = ts.TotalSeconds.ToString();

            if (Configs.GetAPIkey() == "")
            {
                MessageBox.Show("Making a new API key.");
                sr.ident = Configs.Ident;
                sr.time = unixtime;
                sr.clientident = null;
                sr.name = null;
                string json = JsonConvert.SerializeObject(sr);
                sr.MakeRequest("setupkey", "global", json);
            }
            else
            {
                MessageBox.Show("Using " + Configs.GetIdent() + " " + Configs.GetAPIkey());
                sr.ident = Configs.GetIdent();
                sr.time = unixtime;
                sr.player = Configs.GetPlayer();
                string json = JsonConvert.SerializeObject(sr);
                sr.MakeRequest("playerupdate", Configs.GetPlatform(), json);
            }
        }

    }
}