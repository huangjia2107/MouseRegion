using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using DrawRegion.Adorners;
using DrawRegion.Helper;

namespace DrawRegion.Resources.Controls
{
    internal class ResizeThumb : Thumb
    {
        private RotateTransform rotateTransform;
        private double angle;
        private Point transformOrigin;
        private ContentControl designerItem;
        private Canvas canvas;

        public ResizeThumb()
        {
            DragStarted += new DragStartedEventHandler(this.ResizeThumb_DragStarted);
            DragDelta += new DragDeltaEventHandler(this.ResizeThumb_DragDelta);
            DragCompleted += new DragCompletedEventHandler(this.ResizeThumb_DragCompleted);
        }

        private void ResizeThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            this.designerItem = this.DataContext as ContentControl;

            if (this.designerItem != null)
            {
                this.canvas = Algorithm.FindVisualParent<Canvas>(this.designerItem);

                if (this.canvas != null)
                {
                    this.transformOrigin = this.designerItem.RenderTransformOrigin;

                    this.rotateTransform = this.designerItem.RenderTransform as RotateTransform;
                    if (this.rotateTransform != null)
                    {
                        this.angle = this.rotateTransform.Angle * Math.PI / 180.0;
                    }
                    else
                    {
                        this.angle = 0.0d;
                    }
                }
            }
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.designerItem != null)
            {
                double deltaVertical, deltaHorizontal;
                double NewX, NewY;
                double CurX = Canvas.GetLeft(this.designerItem);
                double CurY = Canvas.GetTop(this.designerItem);

                switch (VerticalAlignment)
                {
                    case VerticalAlignment.Bottom:
                        deltaVertical = Math.Min(-e.VerticalChange, this.designerItem.ActualHeight - this.designerItem.MinHeight);
                        NewY = CurY + (this.transformOrigin.Y * deltaVertical * (1 - Math.Cos(-this.angle)));
                        //NewX = Canvas.GetLeft(this.designerItem) -  deltaVertical * this.transformOrigin.Y * Math.Sin(-this.angle);
                        Canvas.SetTop(this.designerItem, Math.Max(0, NewY));
                        // Canvas.SetLeft(this.designerItem, Math.Max(0, NewX));

                        this.designerItem.Height = Math.Min(this.designerItem.Height - deltaVertical, canvas.ActualHeight-NewY);
                        break;
                    case VerticalAlignment.Top:
                        deltaVertical = Math.Min(e.VerticalChange, this.designerItem.ActualHeight - this.designerItem.MinHeight);
                        NewY = CurY + deltaVertical * Math.Cos(-this.angle) + (this.transformOrigin.Y * deltaVertical * (1 - Math.Cos(-this.angle)));
                        // NewX = Canvas.GetLeft(this.designerItem) + deltaVertical * Math.Sin(-this.angle) - (this.transformOrigin.Y * deltaVertical * Math.Sin(-this.angle));
                        Canvas.SetTop(this.designerItem, Math.Max(0, NewY));
                        // Canvas.SetLeft(this.designerItem, Math.Max(0, NewX));

                        this.designerItem.Height += CurY - Math.Max(NewY, 0);
                        break;
                    default:
                        break;
                }

                switch (HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        deltaHorizontal = Math.Min(e.HorizontalChange, this.designerItem.ActualWidth - this.designerItem.MinWidth);
                        //  Canvas.SetTop(this.designerItem, Canvas.GetTop(this.designerItem) + deltaHorizontal * Math.Sin(this.angle) - this.transformOrigin.X * deltaHorizontal * Math.Sin(this.angle));
                        NewX = CurX + deltaHorizontal*Math.Cos(this.angle) + (this.transformOrigin.X*deltaHorizontal*(1 - Math.Cos(this.angle)));
                        Canvas.SetLeft(this.designerItem, Math.Max(0,NewX));

                        this.designerItem.Width += CurX - Math.Max(0, NewX);
                        break;
                    case HorizontalAlignment.Right:
                        deltaHorizontal = Math.Min(-e.HorizontalChange, this.designerItem.ActualWidth - this.designerItem.MinWidth);
                        //  Canvas.SetTop(this.designerItem, Canvas.GetTop(this.designerItem) - this.transformOrigin.X * deltaHorizontal * Math.Sin(this.angle));
                        NewX = CurX + (deltaHorizontal*this.transformOrigin.X*(1 - Math.Cos(this.angle)));
                        Canvas.SetLeft(this.designerItem, Math.Max(0,NewX));

                        this.designerItem.Width = Math.Min(this.designerItem.Width - deltaHorizontal, canvas.ActualWidth - NewX);
                        break;
                    default:
                        break;
                }
            }

            e.Handled = true;
        }

        private void ResizeThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
        }
    }
}