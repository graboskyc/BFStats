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
    public partial class PlayerDetails : PhoneApplicationPage
    {
        public PlayerDetails()
        {
            InitializeComponent();
            DataContext = App.ViewModel;

            PageTitle.Title = App.ViewModel.BuiltPlayer.PlayerName;

            App.ViewModel.SpecsCol.Clear();

            foreach (Specialization s in App.ViewModel.BuiltPlayer.Specs)
            {
                ItemViewModel ivm = new ItemViewModel();
                ivm.LineOne = s.SpecName;
                ivm.PBValue = Convert.ToDouble((Convert.ToDouble(s.Current) / Convert.ToDouble(s.Needed)) * 100.00);
                ivm.ImgSrc = "bf3/" + s.SpecPic;
                ivm.Tag = s.Description;
                ivm.Current = s.Current.ToString();
                ivm.Needed = s.Needed.ToString();
                App.ViewModel.SpecsCol.Add(ivm);
            }

            double d = ((App.ViewModel.BuiltPlayer.Wins * 1.00) / (App.ViewModel.BuiltPlayer.Losses * 1.00));
            double scaled = 0;
            if (d == 0)
            {
                scaled = 50;
            }
            else if (d > 5)
            {
                scaled = 100;
            }
            else if (d > 0)
            {
                scaled = 50 + (d * 10);
            }
            else if (d < -5)
            {
                scaled = 0;
            }
            else
            {
                scaled = 50 - (d * 10);
            }
            string wl = String.Format("{0:0.##}", d);
            string scaledwl = String.Format("{0:0.##}", scaled);
            string uri = "http://chart.googleapis.com/chart?chs=300x150&cht=gom&chd=t:" + scaled + "&chco=939393,2E86B6&chf=bg%2Cs%2CFFFFFF00";

            BitmapImage bmi = new BitmapImage(new Uri(uri, UriKind.Absolute));
            img_wl.Source = bmi;
            txt_wl.Text = wl;

            double kdratio = ((App.ViewModel.BuiltPlayer.Kills * 1.00) / (App.ViewModel.BuiltPlayer.Deaths * 1.00));
            double scaledkd = 0;

            if (kdratio == 1)
            {
                scaledkd = 50;
            }
            else if (kdratio > 5)
            {
                scaledkd = 100;
            }
            else if (kdratio > 1)
            {
                scaledkd = 50 + (kdratio * 10);
            }
            else
            {
                scaledkd = (kdratio / 2) * 100;
            }
            string scaledkdstr = String.Format("{0:0.##}", scaledkd);
            string kduri = "http://chart.googleapis.com/chart?chs=300x150&cht=gom&chd=t:" + scaledkdstr + "&chco=939393,2E86B6&chf=bg%2Cs%2CFFFFFF00";

            BitmapImage kdbmi = new BitmapImage(new Uri(kduri, UriKind.Absolute));
            img_kd.Source = kdbmi;
            string scaledkdstrval = String.Format("{0:0.##}", kdratio);
            txt_kd.Text = scaledkdstrval;

            double acc = ((App.ViewModel.BuiltPlayer.Hits * 1.00) / (App.ViewModel.BuiltPlayer.Shots * 1.00));
            string str_acc = String.Format("{0:0.##}", (acc*100));
            txt_acc.Text = str_acc + "%";

            BitmapImage timebmi = new BitmapImage(new Uri(App.ViewModel.BuiltPlayer.BackTilePic + "150x120", UriKind.Absolute));
            img_kitstime.Source = timebmi;

            double assaultscore = Convert.ToDouble(App.ViewModel.BuiltPlayer.Assault.Score);
            double reconscore = Convert.ToDouble(App.ViewModel.BuiltPlayer.Recon.Score);
            double supportscore = Convert.ToDouble(App.ViewModel.BuiltPlayer.Support.Score);
            double engscore = Convert.ToDouble(App.ViewModel.BuiltPlayer.Engineer.Score);
            double totalscore = assaultscore + reconscore + supportscore + engscore;
            string assaultpers = String.Format("{0:0.##}", ((assaultscore / totalscore) * 100));
            string reconpers = String.Format("{0:0.##}", ((reconscore / totalscore) * 100));
            string supportpers = String.Format("{0:0.##}", ((supportscore / totalscore) * 100));
            string engpers = String.Format("{0:0.##}", ((engscore / totalscore) * 100));
            string scoreuiri = "https://chart.googleapis.com/chart?&cht=pc&chd=t:100|" + assaultpers + "," + engpers + "," + supportpers + "," + reconpers + "&chco=ffffff,308DBF|C4B469|86AE31|A1562E&chs=150x120";
            BitmapImage scorebmi = new BitmapImage(new Uri(scoreuiri, UriKind.Absolute));
            img_kitsscore.Source = scorebmi;

            double spm = (Convert.ToDouble(App.ViewModel.BuiltPlayer.Score) / (Convert.ToDouble(App.ViewModel.BuiltPlayer.TimeRaw) / 60.00));
            txt_spm.Text = String.Format("{0:0.##}", spm);

            double opm = (Convert.ToDouble(App.ViewModel.BuiltPlayer.Objective) / (Convert.ToDouble(App.ViewModel.BuiltPlayer.TimeRaw) / 60.00));
            txt_opm.Text = String.Format("{0:0.##}", opm);

            txt_elo.Text = App.ViewModel.BuiltPlayer.Skill.ToString();

            double timeplayed = (Convert.ToDouble(App.ViewModel.BuiltPlayer.TimeRaw) / 3600.00);
            txt_time.Text = String.Format("{0:0.#}", timeplayed) + " hrs";
        }

        private void spec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count == 1)
            {
                ItemViewModel selectedItem = (ItemViewModel)e.AddedItems[0];
                MessageBox.Show(selectedItem.Tag);
            }
        }
    }
}