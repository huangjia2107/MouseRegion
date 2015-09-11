using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using DrawRegion.Adorners;

namespace DrawRegion
{
    internal class DrawRegionWithMouse
    {
        private Point? startPoint;
        private FrameworkElement element;

        private AdornerLayer adornerLayer;
        private RegionAdorner regionAdorner;
        private RegionParam _regionParam;

        public RegionParam regionParam
        {
            get { return _regionParam; }
        }

        public DrawRegionWithMouse(FrameworkElement _element)
        {
            element = _element;
            InitParameter(element);
        } 

        public void StartDraw()
        {
            if (element == null)
                return;

            startPoint = Mouse.GetPosition(element);
            element.Cursor = Cursors.Cross;
            _regionParam=new RegionParam();
        }

        public void Drawing()
        {
            if (element == null || startPoint == null || regionAdorner == null || _regionParam == null)
                return;

            Point curPoint = Mouse.GetPosition(element);
            ConstructRegionParam(ref _regionParam, startPoint.Value, curPoint);

            regionAdorner.regionParam = _regionParam;
            adornerLayer.Update();
        }

        public void EndDraw()
        {
            startPoint = null;
            _regionParam = null;
            element.Cursor = Cursors.Arrow;

            if (regionAdorner != null && adornerLayer != null)
            {
                regionAdorner.regionParam = null;
                adornerLayer.Update();
            }
        }

        public void Dispose()
        {
            if (adornerLayer != null && regionAdorner != null)
            {
                adornerLayer.Remove(regionAdorner);
                regionAdorner = null;
                adornerLayer = null;
            }

            _regionParam = null;
        }

        private void InitParameter(FrameworkElement _element)
        {
            if (_element == null)
                return;

            adornerLayer = AdornerLayer.GetAdornerLayer(_element);
            regionAdorner = new RegionAdorner(_element);
            adornerLayer.Add(regionAdorner); 
        }

        private void ConstructRegionParam(ref RegionParam _rParam, Point _onePoint, Point _twoPoint)
        {
            _rParam.StartPoint = new Point(Math.Min(_onePoint.X, _twoPoint.X), Math.Min(_onePoint.Y, _twoPoint.Y));
            _rParam.EndPoint = new Point(Math.Max(_onePoint.X, _twoPoint.X), Math.Max(_onePoint.Y, _twoPoint.Y));
        }

    }
}
