using System;
using System.Collections.Generic;
using System.Text;

namespace ImageCharts.Net
{
    /// <summary>
    /// Structure that specifies properties of a line
    /// </summary>
    public struct LineStyle
    {
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
