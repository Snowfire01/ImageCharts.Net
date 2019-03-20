using ImageCharts.Net.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ImageCharts.Net
{
    /// <summary>
    /// Structure that specifies the fill under a line or between lines
    /// </summary>
    public struct LineFill
    {
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

        /// <summary>
        /// Ignored if fill type is <see cref="LineFillType.BetweenLines"/>. Data point indices describing where to start and stio the fill. Default to [first]:[last]
        /// </summary>
        public (int start, int end) FillStartStop { get; set; }
    }
}
