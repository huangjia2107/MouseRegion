using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace DrawRegion.Adorners
{
    internal class DesignerItemAdorner : Adorner
    {
        private VisualCollection visuals;
        private Control control;

        protected override int VisualChildrenCount
        {
            get
            {
                return this.visuals.Count;
            }
        }

        public DesignerItemAdorner(ContentControl designerItem,Control control)
            : base(designerItem)
        {
            SnapsToDevicePixels = true;
            this.control = control;
            this.control.DataContext = designerItem;

            visuals = new VisualCollection(this) {this.control};
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            control.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.visuals[index];
        }
    }
}

