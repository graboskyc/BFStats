using System;
using System.ComponentModel;
using System.Diagnostics;
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
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string _lineOne;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string LineOne
        {
            get
            {
                return _lineOne;
            }
            set
            {
                if (value != _lineOne)
                {
                    _lineOne = value;
                    NotifyPropertyChanged("LineOne");
                }
            }
        }

        private string _imgsrc;
        public string ImgSrc
        {
            get
            {
                return _imgsrc;
            }
            set
            {
                if (value != _imgsrc)
                {
                    _imgsrc = value;
                    NotifyPropertyChanged("ImgSrc");
                }
            }
        }

        private string _img2src;
        public string Img2Src
        {
            get
            {
                return _img2src;
            }
            set
            {
                if (value != _img2src)
                {
                    _img2src = value;
                    NotifyPropertyChanged("Img2Src");
                }
            }
        }

        private string _tag;
        public string Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                if (value != _tag)
                {
                    _tag = value;
                    NotifyPropertyChanged("Tag");
                }
            }
        }

        private string _curr;
        public string Current
        {
            get
            {
                return _curr;
            }
            set
            {
                if (value != _curr)
                {
                    _curr = value;
                    NotifyPropertyChanged("Current");
                }
            }
        }

        private string _needed;
        public string Needed
        {
            get
            {
                return _needed;
            }
            set
            {
                if (value != _needed)
                {
                    _needed = value;
                    NotifyPropertyChanged("Needed");
                }
            }
        }

        private double _pbvalue;
        public double PBValue
        {
            get
            {
                return _pbvalue;
            }
            set
            {
                if (value != _pbvalue)
                {
                    _pbvalue = value;
                    NotifyPropertyChanged("PBValue");
                }
            }
        }

        private string _lineTwo;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string LineTwo
        {
            get
            {
                return _lineTwo;
            }
            set
            {
                if (value != _lineTwo)
                {
                    _lineTwo = value;
                    NotifyPropertyChanged("LineTwo");
                }
            }
        }

        private string _lineThree;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string LineThree
        {
            get
            {
                return _lineThree;
            }
            set
            {
                if (value != _lineThree)
                {
                    _lineThree = value;
                    NotifyPropertyChanged("LineThree");
                }
            }
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