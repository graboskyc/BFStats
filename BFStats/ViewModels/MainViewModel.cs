using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace BFStats
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.WeaponsCol = new ObservableCollection<ItemViewModel>();
            this.VehiclesCol = new ObservableCollection<ItemViewModel>();
            this.UserCol = new ObservableCollection<ItemViewModel>();
            this.RibbonsCol = new ObservableCollection<ItemViewModel>();
            this.MedalsCol = new ObservableCollection<ItemViewModel>();
            this.UnlocksCol = new ObservableCollection<ItemViewModel>();
            this.SpecsCol = new ObservableCollection<ItemViewModel>();
            this.VehicleTypesCol = new ObservableCollection<ItemViewModel>();
            this.VehicleUnlocksCol = new ObservableCollection<ItemViewModel>();
            this.EquipmentsCol = new ObservableCollection<ItemViewModel>();
            this.NextRanksCol = new ObservableCollection<ItemViewModel>();
            this.WS = new WeaponStats();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> WeaponsCol { get; private set; }
        public ObservableCollection<ItemViewModel> VehiclesCol { get; private set; }
        public ObservableCollection<ItemViewModel> UserCol { get; private set; }
        public ObservableCollection<ItemViewModel> RibbonsCol { get; private set; }
        public ObservableCollection<ItemViewModel> MedalsCol { get; private set; }
        public ObservableCollection<ItemViewModel> UnlocksCol { get; private set; }
        public ObservableCollection<ItemViewModel> SpecsCol { get; private set; }
        public ObservableCollection<ItemViewModel> VehicleTypesCol { get; private set; }
        public ObservableCollection<ItemViewModel> VehicleUnlocksCol { get; private set; }
        public ObservableCollection<ItemViewModel> EquipmentsCol { get; private set; }
        public ObservableCollection<ItemViewModel> NextRanksCol { get; private set; }
        public Player BuiltPlayer { get; set; }
        public WeaponStats WS { get; set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            // Sample data; replace with real data
            this.WeaponsCol.Add(new ItemViewModel() { LineOne = "runtime one", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu" });

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}