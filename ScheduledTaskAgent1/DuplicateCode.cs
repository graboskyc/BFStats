using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Shell;
using System.Linq;

namespace ScheduledTaskAgent1
{
    public class DuplicateCode
    {
        public static string GetPlayer()
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            string retstring = "";
            if (s.Contains("Player"))
            {
                retstring = s["Player"].ToString();
            }
            return retstring;
        }
        public static string GetPlatform()
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            string retstring = "360";
            if (s.Contains("Platform"))
            {
                retstring = s["Platform"].ToString();
            }
            return retstring;
        }

        public static void SetPlayerJSON(string p)
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            if (s.Contains("PlayerObject"))
            {
                s["PlayerObject"] = p;
            }
            else
            {
                s.Add("PlayerObject", p);
            }
            s.Save();
        }
        public static string GetPlayerJSON()
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            string p = s["PlayerObject"].ToString();
            return p;
        }

        public static void SetPlayerRank(string r)
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            if (s.Contains("Rank"))
            {
                s["Rank"] = r;
            }
            else
            {
                s.Add("Rank", r);
            }
            s.Save();
        }

        public static int GetScheduleRotation()
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            int ret= 1;
            if (s.Contains("SchedRotation"))
            {
                ret= Convert.ToInt32(s["SchedRotation"].ToString());
            }
            return ret;
        }

        public static void IncScheduleRotation(int r)
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            r++;
            if (r > 3) { r = 1; }
            if (s.Contains("SchedRotation"))
            {
                s["SchedRotation"] = r;
            }
            else
            {
                s.Add("SchedRotation", r);
            }
            s.Save();
        }

        public static string GetPlayerRank()
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            string retstring = "";
            if (s.Contains("Rank"))
            {
                retstring = s["Rank"].ToString();
            }
            return retstring;
        }

        public static string GetAPIkey()
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            string retstring = "";
            if (s.Contains("APIKey"))
            {
                retstring = s["APIKey"].ToString();
            }
            return retstring;
        }

        public static void SetAPIkey(string key)
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            if (s.Contains("APIKey"))
            {
                s["APIKey"] = key;
            }
            else
            {
                s.Add("APIKey", key);
            }
            s.Save();
        }

        public static string GetIdent()
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            string retstring = "";
            if (s.Contains("Ident"))
            {
                retstring = s["Ident"].ToString();
            }
            return retstring;
        }

        public static void SetIdent(string key)
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            if (s.Contains("Ident"))
            {
                s["Ident"] = key;
            }
            else
            {
                s.Add("Ident", key);
            }
            s.Save();
        }

        public static string Ident { get { return "NNiOP9xBtp"; } }
        public static string Secret { get { return "ekTm3i6JnEPGgHHNSBUky8kkbTLDwMDe"; } }

        public static void SetTile(SimplePlayer p)
        {
            ShellTile tile = ShellTile.ActiveTiles.FirstOrDefault();
            if (tile != null)
            {
                StandardTileData newtile = new StandardTileData();
                string frontTile = "bf3/" + p.RankPic;
                newtile.BackgroundImage = new Uri(frontTile, UriKind.Relative);
                newtile.BackTitle = " ";
                newtile.Title = "BF3 Stats";
                newtile.Count = Convert.ToInt32(p.Rank);

                tile.Update(newtile);
            }
        }
    }
}
