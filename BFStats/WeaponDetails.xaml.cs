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
using System.Windows.Media.Imaging;

namespace BFStats
{
    public partial class WeaponDetails : PhoneApplicationPage
    {
        public WeaponDetails()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            App.ViewModel.UnlocksCol.Clear();

            int wid = Convert.ToInt32(NavigationContext.QueryString["WeaponID"].ToString());
            Weapon w = App.ViewModel.BuiltPlayer.Weapons[wid];

            weaponmain.Header = w.WeaponName;
            txt_desc.Text = w.Description;

            BitmapImage bmi = new BitmapImage(new Uri("bf3/" + w.WeaponPic, UriKind.Relative));
            img_weapon.Source = bmi;

            if (w.HasHeatMap)
            {
                heatmap.Visibility = Visibility.Visible;
                BitmapImage heatbmi = w.HeatMap;
                img_heat.Source = heatbmi;
            }
            else
            {
                //heatmap.Visibility = Visibility.Collapsed;
                pivotroot.Items.Remove(heatmap);
            }

            if (w.Unlocks.Count > 0)
            {
                unlocks.Visibility = Visibility.Visible;
                foreach (WeaponUnlock wu in w.Unlocks)
                {
                    ItemViewModel ivm = new ItemViewModel();
                    ivm.LineOne = wu.UnlockName;
                    ivm.PBValue = Convert.ToDouble((Convert.ToDouble(wu.Current) / Convert.ToDouble(wu.Needed)) * 100.00);
                    ivm.Current = wu.Current.ToString();
                    ivm.Needed = wu.Needed.ToString();

                    ivm.ImgSrc = "bf3/" + wu.UnlockPic;
                    ivm.LineOne = wu.UnlockName;
                    ivm.Tag = wu.Description;
                    App.ViewModel.UnlocksCol.Add(ivm);
                }
            }
            else
            {
                //unlocks.Visibility = Visibility.Collapsed;
                pivotroot.Items.Remove(unlocks);
            }

            WeaponStats ws = App.ViewModel.WS;
            if (ws.Stats.ContainsKey(w.WeaponName))
            {
                DmgMax.Text = ws.Stats[w.WeaponName][(int)WeaponStats.Types.dmgMax] + "%";
                DmgMin.Text = ws.Stats[w.WeaponName][(int)WeaponStats.Types.dmgMin] + "%";
                Reload.Text = ws.Stats[w.WeaponName][(int)WeaponStats.Types.rld] + "sec";
                ReloadEmpty.Text = ws.Stats[w.WeaponName][(int)WeaponStats.Types.rldem] + "sec";
                ReloadThresh.Text = ws.Stats[w.WeaponName][(int)WeaponStats.Types.rldtrs] + "sec";
                Suppression.Text = ws.Stats[w.WeaponName][(int)WeaponStats.Types.supp];
                Speed.Text = ws.Stats[w.WeaponName][(int)WeaponStats.Types.speed] + "m/s";
                Distance.Text = ws.Stats[w.WeaponName][(int)WeaponStats.Types.maxdist] + "m";
                Horz.Text = ws.Stats[w.WeaponName][(int)WeaponStats.Types.recoil_l] + "/" + ws.Stats[w.WeaponName][(int)WeaponStats.Types.recoil_r];
                Vert.Text = ws.Stats[w.WeaponName][(int)WeaponStats.Types.recoil_u];
                BTK.Text = ws.Stats[w.WeaponName][(int)WeaponStats.Types.btk_10] + " / " + ws.Stats[w.WeaponName][(int)WeaponStats.Types.btk_50];
                TTK.Text = ws.Stats[w.WeaponName][(int)WeaponStats.Types.ttk_10] + " / " + ws.Stats[w.WeaponName][(int)WeaponStats.Types.ttk_50];
            }
            else
            {
                pivotroot.Items.Remove(fire);
            }

            float hrs = Convert.ToSingle(w.Time) / 3600;
            string timestr = String.Format("{0:0.##}", hrs);
            Time.Text = timestr + " hrs";
            Stars.Text = w.Stars.ToString();
            Hits.Text = w.Hits;
            Shots.Text = w.Shots;
            Kills.Text = w.Kills;
            Headshots.Text = w.Headshots;
            double adj_current = Convert.ToDouble(Convert.ToDouble(100) - (Convert.ToDouble(w.NextStarNeeded) - Convert.ToDouble(w.NextStarCurrent)));
            double adj_needed = Convert.ToDouble(100);
            //starpb.Value = Convert.ToDouble((Convert.ToDouble(w.NextStarCurrent) / Convert.ToDouble(w.NextStarNeeded)) * 100.00);
            starpb.Value = Convert.ToDouble((adj_current / adj_needed) * 100.00);

            Kit.Text = w.InKit.ToString();
            Range.Text = w.Range.ToString();
            ROF.Text = w.ROF.ToString();
            Ammo.Text = w.Ammo.ToString();

            List<string> mode = new List<string>();
            if (w.FireModeAuto) { mode.Add("Auto"); }
            if (w.FireModeBurst) { mode.Add("Burst"); }
            if (w.FireModeSingle) { mode.Add("Single"); }
            Modes.Text = string.Join(", ", mode);
            
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count == 1)
            {
                ItemViewModel selectedItem = (ItemViewModel)e.AddedItems[0];
                MessageBox.Show(selectedItem.Tag);
            }
        }
    }
}