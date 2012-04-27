using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO;
using Microsoft.Phone.Shell;
using Newtonsoft.Json.Linq;
using Microsoft.Phone.Tasks;

namespace BFStats
{
    public class PageHandler
    {
        public static void BuildPage(Player player)
        {
            int i = 0;

            foreach (Weapon w in player.Weapons)
            {
                ItemViewModel ivm = new ItemViewModel();
                if (w.Shots == "0")
                {
                    ivm.LineTwo = "never used";
                }
                else
                {
                    float per = (Convert.ToSingle(w.Hits) / Convert.ToSingle(w.Shots)) * 100;
                    string hitper = String.Format("{0:0.#}", per);
                    float time = Convert.ToSingle(w.Time) / 3600;
                    string timestr = String.Format("{0:0.##}", time);
                    ivm.LineTwo = w.Kills + " kills @ " + hitper + "% acc in " + timestr + " hrs";
                }

                // this is adjusted now so, for example, 733 current with 800 needed shows 33% rather than 92%
                //ivm.PBValue = Convert.ToDouble((Convert.ToDouble(w.Current) / Convert.ToDouble(w.Needed)) * 100.00);
                double adj_current = Convert.ToDouble(Convert.ToDouble(100) - (Convert.ToDouble(w.Needed) - Convert.ToDouble(w.Current)));
                double adj_needed = Convert.ToDouble(100);
                ivm.PBValue = Convert.ToDouble((adj_current / adj_needed) * 100.00);
                ivm.Current = w.Current.ToString();
                ivm.Needed = w.Needed.ToString();

                ivm.ImgSrc = "bf3/" + w.WeaponPic;
                ivm.LineOne = w.WeaponName;
                ivm.Tag = i.ToString();
                i++;
                App.ViewModel.WeaponsCol.Add(ivm);
            }

            foreach (Vehicle v in player.Vehicles)
            {
                ItemViewModel ivm = new ItemViewModel();
                ivm.ImgSrc = "bf3/" + v.VehiclePic;
                ivm.LineOne = v.VehicleName;
                float time = Convert.ToSingle(v.Time) / 3600;
                string timestr = String.Format("{0:0.##}", time);
                ivm.LineTwo = v.Destroys + " destroys & " + v.Kills + " kills in " + timestr + " hrs";
                ivm.Tag = v.Description;

                App.ViewModel.VehiclesCol.Add(ivm);
            }

            foreach (Ribbon r in player.Ribbons)
            {
                ItemViewModel ivm = new ItemViewModel();
                ivm.ImgSrc = "bf3/" + r.RibbonPic;
                ivm.LineOne = r.RibbonName;
                ivm.LineTwo = "acquired " + r.TimesTaken.ToString() + " times";
                ivm.Tag = r.Description;
                App.ViewModel.RibbonsCol.Add(ivm);
            }

            foreach (Medal m in player.Medals)
            {
                ItemViewModel ivm = new ItemViewModel();
                ivm.ImgSrc = "bf3/" + m.MedalPic;
                ivm.LineOne = m.MedalName;
                ivm.LineTwo = "acquired " + m.TimesTaken.ToString() + " times";
                ivm.Tag = m.Description;
                App.ViewModel.MedalsCol.Add(ivm);
            }

            foreach (Equipment eq in player.Equipments)
            {
                ItemViewModel ivm = new ItemViewModel();
                ivm.ImgSrc = "bf3/" + eq.EquipPic;
                ivm.Img2Src = "bf3/" + eq.KitPic;
                ivm.LineOne = eq.EquipName;
                ivm.LineTwo = eq.LineTwo;
                ivm.Tag = eq.Description;
                App.ViewModel.EquipmentsCol.Add(ivm);
            }

            i = 0;
            foreach (VehicleType vt in player.VehicleTypes)
            {
                ItemViewModel ivm = new ItemViewModel();
                ivm.ImgSrc = "bf3/" + vt.TypePic;
                ivm.LineOne = vt.TypeName;
                ivm.Tag = i.ToString();
                i++;
                App.ViewModel.VehicleTypesCol.Add(ivm);
            }

            ItemViewModel ivm_rank = new ItemViewModel();
            ivm_rank.LineOne = player.PlayerName;
            ivm_rank.LineTwo = "rank " + player.Rank.ToString() + " score " + player.Score.ToString("N0");
            ivm_rank.ImgSrc = "bf3/" + player.RankPic;
            ivm_rank.Tag = "Rank";
            App.ViewModel.UserCol.Add(ivm_rank);

            ItemViewModel ivm_assault = new ItemViewModel();
            ivm_assault.LineOne = "Assault";
            ivm_assault.LineTwo = "score " + player.Assault.Score.ToString("N0");
            ivm_assault.ImgSrc = "bf3/" + player.Assault.KitPic;
            App.ViewModel.UserCol.Add(ivm_assault);

            ItemViewModel ivm_eng = new ItemViewModel();
            ivm_eng.LineOne = "Engineer";
            ivm_eng.LineTwo = "score " + player.Engineer.Score.ToString("N0");
            ivm_eng.ImgSrc = "bf3/" + player.Engineer.KitPic;
            App.ViewModel.UserCol.Add(ivm_eng);

            ItemViewModel ivm_recon = new ItemViewModel();
            ivm_recon.LineOne = "Recon";
            ivm_recon.LineTwo = "score " + player.Recon.Score.ToString("N0");
            ivm_recon.ImgSrc = "bf3/" + player.Recon.KitPic;
            App.ViewModel.UserCol.Add(ivm_recon);

            ItemViewModel ivm_sup = new ItemViewModel();
            ivm_sup.LineOne = "Support";
            ivm_sup.LineTwo = "score " + player.Support.Score.ToString("N0");
            ivm_sup.ImgSrc = "bf3/" + player.Support.KitPic;
            App.ViewModel.UserCol.Add(ivm_sup);

            foreach (NextRank nr in player.NextRanks)
            {
                ItemViewModel ivm = new ItemViewModel();

                int curr = nr.RequiredScore - nr.Left;
                ivm.PBValue = Convert.ToDouble(Convert.ToDouble(curr)/Convert.ToDouble(nr.RequiredScore)) * 100.00;
                ivm.Current = curr.ToString();
                ivm.Needed = nr.RequiredScore.ToString();

                ivm.ImgSrc = "bf3/" + nr.RankPic;
                ivm.LineOne = "Lvl " + nr.Level + " " + nr.RankName;
                i++;
                App.ViewModel.NextRanksCol.Add(ivm);
            }

            App.ViewModel.BuiltPlayer = player;
            PageHandler.SetTile();
        }

        public static void SetTile()
        {
            ShellTile tile = ShellTile.ActiveTiles.FirstOrDefault();
            if (tile != null)
            {
                SimplePlayer p = new SimplePlayer(Configs.GetPlayerJSON());
                StandardTileData newtile = new StandardTileData();
                string frontTile = "bf3/" + p.RankPic;
                newtile.BackgroundImage = new Uri(frontTile, UriKind.Relative);
                string backTile = Uri.EscapeUriString(App.ViewModel.BuiltPlayer.BackTilePic + "173x173&chtt=" + Configs.GetPlayer());
                newtile.BackBackgroundImage = new Uri(backTile, UriKind.Absolute);
                newtile.BackTitle = " ";
                newtile.Title = "BF3 Stats";
                newtile.Count = Convert.ToInt32(p.Rank);

                tile.Update(newtile);
            }
        }

        public static void ClearViewModelData()
        {
            App.ViewModel.RibbonsCol.Clear();
            App.ViewModel.UserCol.Clear();
            App.ViewModel.MedalsCol.Clear();
            App.ViewModel.WeaponsCol.Clear();
            App.ViewModel.VehiclesCol.Clear();
            App.ViewModel.VehicleTypesCol.Clear();
            App.ViewModel.VehicleUnlocksCol.Clear();
            App.ViewModel.SpecsCol.Clear();
        }
    }
}
