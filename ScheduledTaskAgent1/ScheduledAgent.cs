using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;
using Microsoft.Phone.Info;
using System.Windows.Threading;
using System.Net;
using Newtonsoft.Json;

namespace ScheduledTaskAgent1
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        private static volatile bool _classInitialized;

        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        public ScheduledAgent()
        {
            if (!_classInitialized)
            {
                _classInitialized = true;
                // Subscribe to the managed exception handler
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    Application.Current.UnhandledException += ScheduledAgent_UnhandledException;
                });
            }
        }

        /// Code to execute on Unhandled Exceptions
        private void ScheduledAgent_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            //ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(30));

            int rotation = DuplicateCode.GetScheduleRotation();
            DuplicateCode.IncScheduleRotation(rotation);

            if (rotation == 2)
            {
                GetAPIData();

                //ShellToast debugtoast = new ShellToast();
                //debugtoast.Title = "BFS";
                //double mem = (DeviceStatus.ApplicationCurrentMemoryUsage / 1000.00);
                //string memstring = String.Format("{0:0.##}", mem);
                //debugtoast.Content = memstring + "MB " + "web pull";
                //debugtoast.Show();

                //DispatcherTimer dt = new DispatcherTimer();
                //dt.Interval = TimeSpan.FromSeconds(7);
                //dt.Tick += new EventHandler(dt_Tick);
                //dt.Start();
            }
            else if (rotation == 1)
            {
                SignedRequest sr = new SignedRequest();
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                string unixtime = ts.TotalSeconds.ToString();
                sr.ident = DuplicateCode.GetIdent();
                sr.time = unixtime;
                sr.player = DuplicateCode.GetPlayer();
                string json = JsonConvert.SerializeObject(sr);
                MakeSignedRequest("playerupdate", DuplicateCode.GetPlatform(), json);
            }
            else
            {
                string oldRank = DuplicateCode.GetPlayerRank();
                SimplePlayer newPlayer = new SimplePlayer(DuplicateCode.GetPlayerJSON());
                if (oldRank != newPlayer.Rank)
                {
                    ShellToast debugtoast = new ShellToast();
                    debugtoast.Title = "BFS";
                    double mem = (DeviceStatus.ApplicationCurrentMemoryUsage / 1000.00);
                    string memstring = String.Format("{0:0.##}", mem);

                    debugtoast.Content = newPlayer.PlayerName + " leveled up to " + newPlayer.Rank;
                    //debugtoast.Content = "O:" + oldRank + " N:" + newPlayer.Rank + " " + memstring;

                    DuplicateCode.SetPlayerRank(newPlayer.Rank);
                    DuplicateCode.SetTile(newPlayer);

                    debugtoast.Show();
                }

                NotifyComplete();
            }
        }

        public void GetAPIData()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                string uri = "http://api.bf3stats.com/" + DuplicateCode.GetPlatform() + "/playerlist/";
                //string uri = "http://10.253.47.251/dev/jsondummy.php";
                string query = "players=" + DuplicateCode.GetPlayer() + "&opt=all";
                WebClient wc = new WebClient();
                wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";
                wc.UploadStringCompleted += new UploadStringCompletedEventHandler(wc_UploadStringCompleted_bg);
                wc.UploadStringAsync(new Uri(uri), "POST", query);
            }
        }

        void wc_UploadStringCompleted_bg(Object sender, UploadStringCompletedEventArgs e)
        {
            string json = (string)e.Result;
            if (json.Length > 100)
            {
                DuplicateCode.SetPlayerJSON(json);
            }
            NotifyComplete();
        }

        public void MakeSignedRequest(string type, string platform, string reqjsondata)
        {
            System.Text.UTF8Encoding en = new System.Text.UTF8Encoding();

            if (DuplicateCode.GetAPIkey() != "")
            {
                string secret = DuplicateCode.GetAPIkey();

                string uri = "http://api.bf3stats.com/" + platform + "/" + type + "/";
                WebClient wc = new WebClient();
                wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";

                wc.UploadStringCompleted += new UploadStringCompletedEventHandler(completed_update);

                System.Security.Cryptography.HMACSHA256 hmac = new System.Security.Cryptography.HMACSHA256(en.GetBytes(secret));

                Byte[] bytes = en.GetBytes(reqjsondata);
                string encodedRequest = Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_').Replace("=", "");

                byte[] hashVal = hmac.ComputeHash(en.GetBytes(encodedRequest));
                string signature = Convert.ToBase64String(hashVal).Replace('+', '-').Replace('/', '_').Replace("=", "");

                string query = "data=" + encodedRequest + "&sig=" + signature;
                wc.UploadStringAsync(new Uri(uri), "POST", query);
            }
        }

        void completed_update(Object sender, UploadStringCompletedEventArgs e)
        {
            string json = (string)e.Result;
            NotifyComplete();
            //if (!json.Contains("error"))
            //{
            //ShellToast debugtoast = new ShellToast();
            //debugtoast.Title = "BFS";
            //debugtoast.Content = "Stats updated";
            //debugtoast.Content = "O:" + oldRank + " N:" + newPlayer.Rank + " " + memstring;

            //debugtoast.Show();
            //}
        }
    }
}