using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace DrawRegion.Adorners
{ 
    internal class RegionParam
    { 
        public Point StartPoint { get; set; } 
        public Point EndPoint { get; set; } 
    }

    internal class RegionAdorner : Adorner
    {
        FrameworkElement element;
        public RegionParam regionParam { get; set; }
        private Pen pen;

        public RegionAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            this.IsHitTestVisible = false;
            this.SnapsToDevicePixels = true;
            element = adornedElement as FrameworkElement;

            pen = GetPen(Brushes.Red, 1, new DashStyle(new double[] { 2, 2 }, 0));
        }

        private Pen GetPen(Brush _brush, double _thickness, DashStyle _dashStyle)
        {
            Pen _pen = new Pen(_brush, _thickness);
            _pen.DashStyle = _dashStyle;
            return _pen;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (regionParam != null)
            {
                Rect rect = new Rect(regionParam.StartPoint.X,
                    regionParam.StartPoint.Y,
                    Math.Abs(regionParam.EndPoint.X - regionParam.StartPoint.X),
                    Math.Abs(regionParam.EndPoint.Y - regionParam.StartPoint.Y));

                drawingContext.DrawRectangle(null, pen, rect);


//                 PolyLineSegment polylineSeg = new PolyLineSegment();
//                 PathSegmentCollection segmentCollection = new PathSegmentCollection() { polylineSeg };
//                 PathFigure pathFigure = new PathFigure() { Segments = segmentCollection, IsClosed=true };
// 
//                 pathFigure.StartPoint = regionParam.StartPoint;
//                     
//                 polylineSeg.Points.Add(new Point(regionParam.EndPoint.X,regionParam.StartPoint.Y));
//                 polylineSeg.Points.Add(regionParam.EndPoint);
//                 polylineSeg.Points.Add(new Point(regionParam.StartPoint.X, regionParam.EndPoint.Y));
// 
//                 PathGeometry pathGeometry = new PathGeometry() { Figures = new PathFigureCollection() { pathFigure } };
//                 drawingContext.DrawGeometry(Brushes.Transparent, pen, pathGeometry);
            }

        }
    }
}
