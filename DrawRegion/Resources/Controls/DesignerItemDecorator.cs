using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using DrawRegion.Adorners;
using DrawRegion.Helper;

namespace DrawRegion.Resources.Controls
{
    internal class DesignerItemDecorator : Control
    {
        private Adorner adorner;

        public static readonly DependencyProperty ShowDecoratorProperty =
            DependencyProperty.Register("ShowDecorator", typeof(bool), typeof(DesignerItemDecorator), new PropertyMetadata(false, ShowDecoratorProperty_Changed));
        public bool ShowDecorator
        {
            get { return (bool)GetValue(ShowDecoratorProperty); }
            set { SetValue(ShowDecoratorProperty, value); }
        }

        private void HideAdorner()
        {
            if (this.adorner != null)
            {
                this.adorner.Visibility = Visibility.Collapsed;
            }
        }

        private void ShowAdorner()
        {
            if (adorner == null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);

                if (adornerLayer != null)
                {
                    ContentControl designerItem = this.DataContext as ContentControl;
                    Canvas canvas = Algorithm.FindVisualParent<Canvas>(designerItem);
                    adorner = new DesignerItemAdorner(designerItem,new ResizeRotateControl());
                    adornerLayer.Add(adorner);

                    if (ShowDecorator)
                    {
                        adorner.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        adorner.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else
            {
                adorner.Visibility = Visibility.Visible;
            }
        }

        private static void ShowDecoratorProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DesignerItemDecorator decorator = (DesignerItemDecorator)d;
            bool showDecorator = (bool)e.NewValue; 

            if (showDecorator)
                decorator.ShowAdorner();
            else
                decorator.HideAdorner();
        }
    }
}
