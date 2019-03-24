using System;
using System.Collections.Generic;
using System.Text;

namespace ImageCharts.Net
{
    /// <summary>
    /// Structure that specifies properties of a line in a line chart
    /// </summary>
    public struct LineStyle
    {
        /// <summary>
        /// Creates a new instance of <see cref="LineStyle"/>
        /// </summary>
        /// <param name="thickness">The thickness of the line in pixels</param>
        /// <param name="dashLength">Optional. Makes line dashed if defined. Specifies the length of one dash in the line in pixels</param>
        /// <param name="spaceLength">Optional. Only works if line is dashed. Specifies the length of the space between two dashes in pixels</param>
        public LineStyle(int thickness, int? dashLength, int? spaceLength)
        {
            this.Thickness = thickness;
            this.DashLength = dashLength;
            this.SpaceLength = spaceLength;
        }

        /// <summary>
        /// The thickness of the line in pixels
        /// </summary>
        public int Thickness { get; set; }

        /// <summary>
        /// Optional. Makes line dashed if defined. Specifies the length of one dash in the line in pixels
        /// </summary>
        public int? DashLength { get; set; }

        /// <summary>
        /// Optional. Only works if line is dashed. Specifies the length of the space between two dashes in pixels
        /// </summary>
        public int? SpaceLength { get; set; }
    }
}
