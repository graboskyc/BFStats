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

namespace BFStats
{
    public class Configs
    {
        public static void SetPlayer(string name)
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            if (s.Contains("Player"))
            {
                s["Player"] = name;
            }
            else
            {
                s.Add("Player", name);
            }
        }
        public static void SetPlatform(string name)
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            if (name == "xbox")
            {
                name = "360";
            }
            if (s.Contains("Platform"))
            {
                s["Platform"] = name;
            }
            else
            {
                s.Add("Platform", name);
            }
        }
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

        public static void SetFirstLoad(bool which)
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            if (s.Contains("FirstLoad"))
            {
                s["FirstLoad"] = which;
            }
            else
            {
                s.Add("FirstLoad", which);
            }
        }

        public static bool GetFirstLoad()
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            bool retstring = true;
            if (s.Contains("FirstLoad"))
            {
                retstring = Convert.ToBoolean(s["FirstLoad"].ToString());
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

        public static void SetPlayerJSON(string p)
        {
            //MessageBox.Show("stored");
            var s = IsolatedStorageSettings.ApplicationSettings;
            if (s.Contains("PlayerObject"))
            {
                s["PlayerObject"] =  p;
            }
            else
            {
                s.Add("PlayerObject", p);
            }
        }
        public static string GetPlayerJSON()
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            string p = s["PlayerObject"].ToString();
            return p;
        }

        public static bool HasPlayerObject()
        {
            bool retval = false;
            var s = IsolatedStorageSettings.ApplicationSettings;
            if (s.Contains("PlayerObject"))
            {
                if (s["PlayerObject"].ToString().Length > 100)
                {
                    retval = true;
                }
            }
            return retval;
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
        public static int GetScheduleRotation()
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            int ret = 1;
            if (s.Contains("SchedRotation"))
            {
                ret = Convert.ToInt32(s["SchedRotation"].ToString());
            }
            return ret;
        }
        public static void ClearSettings()
        {
            var s = IsolatedStorageSettings.ApplicationSettings;
            if (s.Contains("Rank"))
            {
                s.Remove("Rank");
            }
            if (s.Contains("PlayerObject"))
            {
                s.Remove("PlayerObject");
            }
            if (s.Contains("Ident"))
            {
                s.Remove("Ident");
            }
            if (s.Contains("APIKey"))
            {
                s.Remove("APIKey");
            }
            if (s.Contains("FirstLoad"))
            {
                s.Remove("FirstLoad");
            }
            if (s.Contains("Player"))
            {
                s.Remove("Player");
            }
            if (s.Contains("Platform"))
            {
                s.Remove("Platform");
            }
            if (s.Contains("SchedRotation"))
            {
                s.Remove("SchedRotation");
            }
            s.Save();
        }
    }
}
