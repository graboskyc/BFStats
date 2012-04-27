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

namespace BFStats
{
    public class VehicleType : IComparable<VehicleType>
    {
        public string TypeName { get; set; }
        public string TypePic { get; set; }
        public string Description { get; set; }
        public int Kills { get; set; }
        public int Score { get; set; }
        public string TimeRaw { get; set; }
        public int ServiceStars { get; set; }
        public int SSCurrent { get; set; }
        public int SSNeeded { get; set; }
        public List<VehicleUnlock> Unlocks { get; set; }

        public int CompareTo(VehicleType obj)
        {
            return TypeName.CompareTo(obj.TypeName);
        }
    }
}
