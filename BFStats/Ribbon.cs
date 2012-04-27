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
    public class Ribbon : IComparable<Ribbon>
    {
        public string RibbonName { get; set; }
        public int TimesTaken { get; set; }
        public string RibbonPic { get; set; }
        public string Description { get; set; }

        public int CompareTo(Ribbon obj)
        {
            return RibbonName.CompareTo(obj.RibbonName);
        }
    }
}
