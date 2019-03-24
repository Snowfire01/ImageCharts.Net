using ImageCharts.Net.Enums;
using System.Drawing;

namespace ImageCharts.Net
{
    /// <summary>
    /// Structure that specifies the fill under a line or between lines
    /// </summary>
    public struct LineFill
    {
        /// <summary>
        /// Creates a new instance of <see cref="LineFill"/>
        /// </summary>
        /// <param name="lineFillType">The type of fill. Can either be from a line to the base of the chart or between two lines (between two lines is not yet supported)</param>
        /// <param name="color">The color of the fill</param>
        /// <param name="endLineIndex">Ignored if fill type is <see cref="LineFillType.UnderLine"/>. The line at which to stop the fill. This line must be below the current line.</param>
        public LineFill(LineFillType lineFillType, Color color, int endLineIndex = 0)
        {
            this.LineFillType = lineFillType;
            this.Color = color;
            this.EndLineIndex = endLineIndex;
        }

        /// <summary>
        /// The type of fill. Can either be from a line to the base of the chart or between two lines (between two lines is not yet supported)
        /// </summary>
        public LineFillType LineFillType { get; set; }

        /// <summary>
        /// The color of the fill
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Ignored if fill type is <see cref="LineFillType.UnderLine"/>. The line at which to stop the fill. This line must be below the current line.
        /// </summary>
        public int EndLineIndex { get; set; }
    }
}
