using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using DrawRegion.Adorners;
using DrawRegion.Models;

namespace DrawRegion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DrawRegionWithMouse drawRegion = null;
        private ObservableCollection<RegionSetting> regionCollection = null;

        public MainWindow()
        {
            InitializeComponent();

            regionCollection = new ObservableCollection<RegionSetting>();
            RegionListBox.ItemsSource = regionCollection;
        }

        private void RegionListBox_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (drawRegion != null)
                drawRegion.StartDraw();
        }

        private void RegionListBox_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (EditBtn.IsChecked == true && drawRegion != null)
            {
                if (drawRegion.regionParam != null)
                {
                    RegionSetting regionSetting = GetRegionSetting(drawRegion.regionParam);
                    if (regionSetting.Width > 20 && regionSetting.Height > 20)
                    {
                        regionSetting.IsSelected = true;
                        regionCollection.Add(regionSetting);
                    }

                    drawRegion.EndDraw();    
                }
            }
        }

        private RegionSetting GetRegionSetting(RegionParam _regionParam)
        {
            RegionSetting rs = new RegionSetting
            {
                X = _regionParam.StartPoint.X,
                Y = _regionParam.StartPoint.Y,
                Width = Math.Abs(_regionParam.StartPoint.X - _regionParam.EndPoint.X),
                Height = Math.Abs(_regionParam.StartPoint.Y - _regionParam.EndPoint.Y)
            };

            return rs;
        }

        private void RegionListBox_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (drawRegion != null)
                drawRegion.Drawing();
        }

        private void RegionListBox_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (drawRegion != null)
                drawRegion.EndDraw();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (EditBtn.IsChecked == true)
            {
                drawRegion = new DrawRegionWithMouse(RegionListBox);
            }
            else
            {
                drawRegion.Dispose();
                drawRegion = null;
            }
        }
    }
}
