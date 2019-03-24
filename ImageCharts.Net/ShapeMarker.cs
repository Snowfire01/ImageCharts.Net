using ImageCharts.Net.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ImageCharts.Net
{
    public struct ShapeMarker
    {
        public ShapeMarkerType ShapeMarkerType { get; set; }

        public Color Color { get; set; }

        public int Size { get; set; }
    }
}
