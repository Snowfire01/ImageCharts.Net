using System;
using System.Collections.Generic;
using System.Text;

namespace ImageCharts.Net.ChartProperties
{
    /// <summary>
    /// Contains properties that describe grid lines in a bar or line chart
    /// </summary>
    public struct GridLines
    {
        /// <summary>
        /// Creates a new instance of <see cref="GridLines"/>
        /// </summary>
        /// <param name="showVerticalGridLines">Determines whether the graph will show vertical grid lines</param>
        /// <param name="showHorizontalGridLines">Determines whether the graph will show horizontal grid lines</param>
        /// <param name="dashLength">Optional. Define if you want dashed grid lines. Specifies the length of a single line dash in pixels. Default is 4, set to 0 for no dashes.</param>
        /// <param name="spaceLength">Optional. Define if you want dashed grid lines. Specifies the spacing between dashes. Default is 1, set to 0 for no spaces.</param>
        public GridLines(bool showVerticalGridLines, bool showHorizontalGridLines, int? dashLength = 4, int? spaceLength = 1)
        {
            this.ShowVerticalGridLines = showVerticalGridLines;
            this.ShowHorizontalGridLines = showHorizontalGridLines;
            this.DashLength = dashLength;
            this.SpaceLength = spaceLength;
        }

        /// <summary>
        /// Determines whether the graph will show vertical grid lines
        /// </summary>
        public bool ShowVerticalGridLines { get; set; }

        /// <summary>
        /// Determines whether the graph will show horizontal grid lines
        /// </summary>
        public bool ShowHorizontalGridLines { get; set; }

        /// <summary>
        /// Optional. Define if you want dashed grid lines. Specifies the length of a single line dash in pixels. Default is 4, set to 0 for no dashes.
        /// </summary>
        public int? DashLength { get; set; }

        /// <summary>
        /// Optional. Define if you want dashed grid lines. Specifies the spacing between dashes. Default is 1, set to 0 for no spaces.
        /// </summary>
        public int? SpaceLength { get; set; }
    }
}
