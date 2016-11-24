using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;

namespace SnowFur.Views.Controls
{
	public class GoogleMap : DotvvmMarkupControl
	{
        public decimal Longitude
        {
            get { return (decimal)GetValue(LongitudeProperty); }
            set { SetValue(LongitudeProperty, value); }
        }
        public static readonly DotvvmProperty LongitudeProperty
                = DotvvmProperty.Register<decimal, GoogleMap>(c => c.Longitude);

        public decimal Latitude
        {
            get { return (decimal)GetValue(LatitudeProperty); }
            set { SetValue(LatitudeProperty, value); }
        }
        public static readonly DotvvmProperty LatitudeProperty
                = DotvvmProperty.Register<decimal, GoogleMap>(c => c.Latitude);

        public int Zoom
        {
            get { return (int)GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }
        public static readonly DotvvmProperty ZoomProperty
                = DotvvmProperty.Register<int, GoogleMap>(c => c.Zoom);

        public string PointTitle
        {
            get { return (string)GetValue(PointTitleProperty); }
            set { SetValue(PointTitleProperty, value); }
        }
        public static readonly DotvvmProperty PointTitleProperty
                = DotvvmProperty.Register<string, GoogleMap>(c => c.PointTitle);

    }
}

