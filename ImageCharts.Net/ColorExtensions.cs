using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ImageCharts.Net
{
    internal static class ColorExtensions
    {
        internal static string GetHexString(this Color color) => $"{color.R:X2}{color.G:X2}{color.B:X2}{color.A:X2}";
    }
}
