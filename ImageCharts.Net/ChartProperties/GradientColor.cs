using System.Drawing;

namespace ImageCharts.Net.ChartProperties
{
    /// <summary>
    /// Contains properties for a gradient color fill
    /// </summary>
    public class GradientColor
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

        /// <summary>
        /// Creates a new instance of a <see cref="GradientColor"/>
        /// </summary>
        /// <param name="color">The color to be used for the gradient. May include a transparency value.</param>
        /// <param name="centerPoint">specifies the anchor point for the color. The color will start to fade from this point as it approaches another anchor. 
        /// The value range is from 0.0 (bottom or left edge) to 1.0 (top or right edge), tilted at the angle specified in the Gradient.</param>
        public GradientColor(Color color, double centerPoint)
        {
            this.Color = color;
            this.CenterPoint = centerPoint;
        }
    }
}
