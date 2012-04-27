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
using System.Windows.Media.Imaging;

namespace BFStats
{
    public static class HeatMap
    {
        public static bool HasMap(string name)
        {
            bool hasmap = true;
            string uri = GetURI(name);
            if (uri.Contains("placeholder"))
            {
               hasmap = false;
            }
            return hasmap;
        }
        private static string GetURI(string name)
        {
            string uri = "";
            //string prefix = "http://symthic.com/sivut/img/plot/";
            string prefix = "http://gator1477.hostgator.com/~symthic/symv20/images/plots/bf3_plots_218/";
            switch (name)
            {
                case "AK-74M":
                    uri = prefix + "AK-74M_bf3_.png";
                    break;
                case "M16A4":
                case "M16A3":
                    uri = prefix + "M16A3_bf3_.png";
                    break;
                case "M416":
                    uri = prefix + "M416_bf3_.png";
                    break;
                case "AEK-971":
                    uri = prefix + "AEK-971_bf3_.png";
                    break;
                case "F2000":
                    uri = prefix + "F2000_bf3_.png";
                    break;
                case "FAMAS":
                    uri = prefix + "FAMAS_bf3_.png";
                    break;
                case "L85A2":
                    uri = prefix + "L85A2_bf3_.png";
                    break;
                case "AN-94":
                    uri = prefix + "AN-94_bf3_.png";
                    break;
                case "G3A3":
                    uri = prefix + "G3A3_bf3_.png";
                    break;
                case "G53":
                    uri = prefix + "G53_bf3_.png";
                    break;
                case "KH2002":
                    uri = prefix + "KH2002_bf3_.png";
                    break;
                case "M4":
                case "M4A1":
                    uri = prefix + "M4A1_bf3_.png";
                    break;
                case "AKS-74u":
                    uri = prefix + "AKS-74u_bf3_.png";
                    break;
                case "SG553":
                    uri = prefix + "SG553_bf3_.png";
                    break;
                case "A-91":
                    uri = prefix + "A-91_bf3_.png";
                    break;
                case "G36C":
                    uri = prefix + "G36C_bf3_.png";
                    break;
                case "SCAR-H":
                    uri = prefix + "SCAR-H_bf3_.png";
                    break;
                case "M27 IAR":
                    uri = prefix + "M27%20IAR_bf3_.png";
                    break;
                case "RPK-74M":
                    uri = prefix + "RPK-74M_bf3_.png";
                    break;
                case "M249":
                    uri = prefix + "M249_bf3_.png";
                    break;
                case "Type 88 LMG":
                    uri = prefix + "Type%2088_bf3_.png";
                    break;
                case "PKP PECHENEG":
                    uri = prefix + "PKP_bf3_.png";
                    break;
                case "M240B":
                    uri = prefix + "M240B_bf3_.png";
                    break;
                case "M60E4":
                    uri = prefix + "M60_bf3_.png";
                    break;
                case "SVD":
                    uri = prefix + "SVD_bf3_.png";
                    break;
                case "L96":
                    uri = prefix + "L96_bf3_.png";
                    break;
                case "SV98":
                    uri = prefix + "SV98_bf3_.png";
                    break;
                case "M98B":
                    uri = prefix + "M98B_bf3_.png";
                    break;
                case "M40A5":
                    uri = prefix + "M40A5_bf3_.png";
                    break;
                case "MK11 MOD 0":
                    uri = prefix + "MK11_bf3_.png";
                    break;
                case "M39 EMR":
                    uri = prefix + "M39%20EBR_bf3_.png";
                    break;
                case "MG36":
                    uri = prefix + "MG36_bf3_.png";
                    break;
                case "QBB-95":
                    uri = prefix + "QBB-95_bf3_.png";
                    break;
                case "QBU-88":
                    uri = prefix + "QBU-88_bf3_.png";
                    break;
                case "SKS":
                    uri = prefix + "SKS_bf3_.png";
                    break;
                case "PP-2000":
                    uri = prefix + "PP-2000_bf3_.png";
                    break;
                case "PP-19":
                    uri = prefix + "PP-19_bf3_.png";
                    break;
                case "UMP-45":
                    uri = prefix + "UMP-45_bf3_.png";
                    break;
                case "MP7":
                    uri = prefix + "MP7_bf3_.png";
                    break;
                case "AS VAL":
                    uri = prefix + "AS%20VAL_bf3_.png";
                    break;
                case "PDW-R":
                    uri = prefix + "PDW-R_bf3_.png";
                    break;
                case "P90":
                    uri = prefix + "P90_bf3_.png";
                    break;
                case "QBZ-95B":
                    uri = prefix + "QBZ-95B_bf3_.png";
                    break;
                //case "G17C":
                //case "G17C SUPP.":
                //    uri = prefix + "P_G17C.png";
                //    break;
                case "MP412 REX":
                    uri = prefix + "MP412_bf3_.png";
                    break;
                case "M1911 SUPP.":
                case "M1911 TACT.":
                case "M1911 S-TAC":
                case "M9 SUPP.":
                case "M1911":
                case "M9":
                case "M9 TACT.":
                    uri = prefix + "M1911_bf3_.png";
                    break;
                case "MP443 SUPP.":
                case "MP443":
                    uri = prefix + "MP443_bf3_.png";
                    break;
                case "G18 SUPP.":
                case "G18":
                    uri = prefix + "G18_bf3_.png";
                    break;
                //case "93R":
                //    uri = prefix + "P_93R.png";
                //   break;
                default:
                    uri = "bf3/soldierspecializations/placeholder.png";
                    break;
            }

            return uri;
        }
        public static BitmapImage Lookup(string name)
        {
            string uri = GetURI(name);
            BitmapImage heatbmi = new BitmapImage(new Uri(uri, UriKind.RelativeOrAbsolute));
            return heatbmi;
        }
    }
}
