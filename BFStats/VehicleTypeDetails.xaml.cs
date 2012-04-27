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
    public partial class VehicleTypeDetails : PhoneApplicationPage
    {
        public VehicleTypeDetails()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                App.ViewModel.VehicleUnlocksCol.Clear();

                int vtid = Convert.ToInt32(NavigationContext.QueryString["VehicleTypeID"].ToString());
                VehicleType vt = App.ViewModel.BuiltPlayer.VehicleTypes[vtid];

                txt_desc.Text = vt.TypeName;
                BitmapImage bmi = new BitmapImage(new Uri("bf3/" + vt.TypePic, UriKind.Relative));
                img_type.Source = bmi;

                foreach (VehicleUnlock vu in vt.Unlocks)
                {
                    ItemViewModel ivm = new ItemViewModel();
                    ivm.LineOne = vu.UnlockName;
                    if (Convert.ToDouble(vu.Needed) == 0)
                    {
                        ivm.PBValue = 100.00;
                    }
                    else
                    {
                        ivm.PBValue = Convert.ToDouble((Convert.ToDouble(vu.Current) / Convert.ToDouble(vu.Needed)) * 100.00);
                    }
                    ivm.Needed = vu.Needed.ToString();
                    ivm.Current = vu.Current.ToString();
                    ivm.Tag = vu.Description;

                    ivm.ImgSrc = "bf3/" + vu.UnlockPic;
                    App.ViewModel.VehicleUnlocksCol.Add(ivm);
                }

                Kills.Text = vt.Kills.ToString();
                Stars.Text = vt.ServiceStars.ToString();
                starpb.Value = Convert.ToDouble((Convert.ToDouble(vt.SSCurrent) / Convert.ToDouble(vt.SSNeeded)) * 100.00);
                float hrs = Convert.ToSingle(vt.TimeRaw) / 3600;
                string timestr = String.Format("{0:0.##}", hrs);
                Time.Text = timestr + " hrs";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

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