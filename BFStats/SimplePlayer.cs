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
using System.Collections.Generic;

namespace BFStats
{
    public class SimplePlayer
    {
        public string PlayerName;
        public string Rank;
        public int Score;
        public int LastUpdate;
        public string RankPic;

        public SimplePlayer(string json)
        {
            JObject data = JObject.Parse(json);

            this.PlayerName = data["list"][Configs.GetPlayer()]["name"].ToString();
            this.Rank = data["list"][Configs.GetPlayer()]["stats"]["rank"]["nr"].ToString();
            this.Score = Convert.ToInt32(data["list"][Configs.GetPlayer()]["stats"]["scores"]["score"].ToString());
            this.LastUpdate = Convert.ToInt32(data["list"][Configs.GetPlayer()]["date_update"].ToString());
            this.RankPic = data["list"][Configs.GetPlayer()]["stats"]["rank"]["img_small"].ToString();
        }
    }
}
