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
using Newtonsoft.Json.Linq;

namespace BFStats
{
    public class SignedRequest
    {
        public string ident { get; set; }
        public string time { get; set; }
        public string name { get; set; }
        public string clientident { get; set; }
        public string player { get; set; }
        public SignedRequest()
        {

        }
        public void MakeRequest(string type, string platform, string reqjsondata)
        {
            System.Text.UTF8Encoding en = new System.Text.UTF8Encoding();

            string secret = Configs.Secret;

            string uri = "http://api.bf3stats.com/" + platform + "/" + type + "/";
            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";

            if (type == "setupkey")
            {
                wc.UploadStringCompleted += new UploadStringCompletedEventHandler(completed_register);
            }
            else
            {
                wc.UploadStringCompleted += new UploadStringCompletedEventHandler(completed_update);
                secret = Configs.GetAPIkey();
            }

            System.Security.Cryptography.HMACSHA256 hmac = new System.Security.Cryptography.HMACSHA256(en.GetBytes(secret));

            Byte[] bytes = en.GetBytes(reqjsondata);
            string encodedRequest = Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_').Replace("=", "");

            byte[] hashVal = hmac.ComputeHash(en.GetBytes(encodedRequest));
            string signature = Convert.ToBase64String(hashVal).Replace('+', '-').Replace('/', '_').Replace("=", "");

            string query = "data=" + encodedRequest + "&sig=" + signature;
            wc.UploadStringAsync(new Uri(uri), "POST", query);
        }

        void completed_register(Object sender, UploadStringCompletedEventArgs e)
        {
            string json = (string)e.Result;
            if (json.Length < 5)
            {
                MessageBox.Show("BF3Stats.com is down. Cannot download your statistics. Try back later.");
            }
            else
            {
                try
                {
                    JObject data = JObject.Parse(json);
                    if (data["status"].ToString() == "ok")
                    {
                        Configs.SetIdent(data["ident"].ToString());
                        Configs.SetAPIkey(data["key"].ToString());
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
            }
        }

        void completed_update(Object sender, UploadStringCompletedEventArgs e)
        {
            string json = (string)e.Result;
            if (json.Length < 5)
            {
                MessageBox.Show("BF3Stats.com is down. Cannot download your statistics. Try back later.");
            }
            else
            {
                try
                {
                    if (!json.Contains("error"))
                    {
                        MessageBox.Show("Your stats have been updated on the server. Request a pull.");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
