using System.Drawing;

namespace ImageCharts.Net.Extensions
{
    internal static class ColorExtension
    {
        /// <summary>
        /// Returns a hexadecimal representation of the color WITHOUT a '#' character in the beginning
        /// </summary>
        internal static string GetHexString(this Color color) => $"{color.R:X2}{color.G:X2}{color.B:X2}{color.A:X2}";
    }
}
