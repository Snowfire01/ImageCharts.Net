using System.Drawing;

namespace ImageCharts.Net
{
    public struct GradientColor
    {
        /// <summary>
        /// The color to be used for the gradient. May include a transparency value.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// specifies the anchor point for the color. The color will start to fade from this point as it approaches another anchor. 
        /// The value range is from 0.0 (bottom or left edge) to 1.0 (top or right edge), tilted at the angle specified in the Gradient.
        /// </summary>
        public double CenterPoint { get; set; }
    }
}
