using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using DrawRegion.Helper;

namespace DrawRegion.Resources.Controls
{
    internal class MoveThumb : Thumb
    {
        private RotateTransform rotateTransform;
        private ContentControl designerItem;
        private Canvas canvas;

        public MoveThumb()
        { 
            DragStarted += MoveThumb_DragStarted;
            DragDelta += MoveThumb_DragDelta; 
        } 

        private void MoveThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            this.designerItem = DataContext as ContentControl; 

            if (this.designerItem != null)
            {
                Selector.SetIsSelected(designerItem, true);
                designerItem.Focus();
                designerItem.PreviewKeyDown -= designerItem_PreviewKeyDown;
                designerItem.PreviewKeyDown += designerItem_PreviewKeyDown;

                this.rotateTransform = this.designerItem.RenderTransform as RotateTransform;
                canvas = Algorithm.FindVisualParent<Canvas>(this.designerItem);
            }
        }

        void designerItem_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    Move(new Point(-1, 0));
                    break;
                case Key.Right:
                    Move(new Point(1, 0));
                    break;
                case Key.Up:
                    Move(new Point(0, -1));
                    break;
                case Key.Down:
                    Move(new Point(0, 1));
                    break;
            }
            e.Handled = true;
        } 

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.designerItem != null)
            {
                Point dragDelta = new Point(e.HorizontalChange, e.VerticalChange);
                Move(dragDelta);
            }
        }

        private void Move(Point delta)
        {
            if (this.rotateTransform != null)
            {
                delta = this.rotateTransform.Transform(delta);
            }

            double NewX = Canvas.GetLeft(this.designerItem) + delta.X;
            double NewY = Canvas.GetTop(this.designerItem) + delta.Y;

            Canvas.SetLeft(this.designerItem, Math.Min(Math.Max(NewX, 0), canvas.ActualWidth - this.designerItem.ActualWidth));
            Canvas.SetTop(this.designerItem, Math.Min(Math.Max(NewY, 0), canvas.ActualHeight - this.designerItem.ActualHeight));
        }
    }
}
