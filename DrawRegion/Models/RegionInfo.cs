using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrawRegion.Models
{
    internal class RegionSetting : BaseInfo
    {
        private double _X;
        public double X
        {
            get { return _X; }
            set { _X = value; InvokePropertyChanged("X"); }
        }

        private double _Y;
        public double Y
        {
            get { return _Y; }
            set { _Y = value; InvokePropertyChanged("Y"); }
        }

        private double _Width;
        public double Width
        {
            get { return _Width; }
            set { _Width = value; InvokePropertyChanged("Width"); }
        }

        private double _Height;
        public double Height
        {
            get { return _Height; }
            set { _Height = value; InvokePropertyChanged("Height"); }
        }

        private bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { _IsSelected = value; InvokePropertyChanged("IsSelected"); }
        }

        private bool _IsEditMode;
        public bool IsEditMode
        {
            get { return _IsEditMode; }
            set { _IsEditMode = value; InvokePropertyChanged("IsEditMode"); }
        }
    }
}
