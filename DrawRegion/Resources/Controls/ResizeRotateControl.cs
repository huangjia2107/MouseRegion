using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DrawRegion.Resources.Controls
{
    internal class ResizeRotateControl:Control
    {
        static ResizeRotateControl()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizeRotateControl), new FrameworkPropertyMetadata(typeof(ResizeRotateControl)));
        }
    }
}
