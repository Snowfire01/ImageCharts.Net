using System.Drawing;

namespace ImageCharts.Net.ChartProperties
{
    /// <summary>
    /// Describes a single color fill without any special effects
    /// </summary>
    public class SingleColorFill : Fill
    {
        /// <summary>
        /// The color of the fill
        /// </summary>
        public Color Color { get; set; }
    }
}
