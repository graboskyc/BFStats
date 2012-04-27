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

namespace BFStats
{
    public class Medal : IComparable<Medal>
    {
        public string MedalName { get; set; }
        public int TimesTaken { get; set; }
        public string MedalPic { get; set; }
        public string Description { get; set; }

        public int CompareTo(Medal obj)
        {
            return MedalName.CompareTo(obj.MedalName);
        }
    }
}
