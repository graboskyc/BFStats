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
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace BFStats
{
    public class Weapon : IComparable<Weapon>
    {
        public string Time { get; set; }
        public string Kills { get; set; }
        public string Headshots { get; set; }
        public string Hits { get; set; }
        public string Shots { get; set; }
        public string Stars { get; set; }
        public string WeaponName { get; set; }
        public string WeaponPic { get; set; }
        public int Current { get; set; }
        public int Needed { get; set; }
        public string Description { get; set; }
        public int NextStarCurrent { get; set; }
        public int NextStarNeeded { get; set; }

        public string InKit { get; set; }
        public string Range { get; set; }
        public bool FireModeAuto { get; set; }
        public string ROF { get; set; }
        public bool FireModeBurst { get; set; }
        public bool FireModeSingle { get; set; }
        public string Ammo { get; set; }

        public List<WeaponUnlock> Unlocks { get; set; }

        public BitmapImage HeatMap
        {
            get { return BFStats.HeatMap.Lookup(this.WeaponName); }
        }

        public bool HasHeatMap
        {
            get { return BFStats.HeatMap.HasMap(this.WeaponName); }
        }

        public int CompareTo(Weapon obj)
        {
            return WeaponName.CompareTo(obj.WeaponName);
        }
    }
}
