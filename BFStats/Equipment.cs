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
    public class Equipment : IComparable<Equipment>
    {
        public string EquipName { get; set; }
        public string Description { get; set; }
        public string EquipPic { get; set; }
        public string KitPic { get; set; }
        public string LineTwo { get; set; }

        public int CompareTo(Equipment obj)
        {
            return EquipName.CompareTo(obj.EquipName);
        }
    }
}
