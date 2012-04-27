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
    public class WeaponUnlock
    {
        public string UnlockName { get; set; }
        public int Current { get; set; }
        public int Needed { get; set; }
        public string UnlockPic { get; set; }
        public string Description { get; set; }
    }
}
