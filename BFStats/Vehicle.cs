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
    public class Vehicle : IComparable<Vehicle>
    {
        public string Time { get; set; }
        public string Kills { get; set; }
        public string Destroys { get; set; }
        public string VehicleName { get; set; }
        public string VehiclePic { get; set; }
        public string Description { get; set; }
        public string TypeKey { get; set; }

        public int CompareTo(Vehicle obj)
        {
            return VehicleName.CompareTo(obj.VehicleName);
        }
    }
}
