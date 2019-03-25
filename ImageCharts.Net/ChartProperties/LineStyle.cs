namespace ImageCharts.Net.ChartProperties
{
    /// <summary>
    /// Describes the properties of a line in a line chart
    /// </summary>
    public class LineStyle
    {
        /// <summary>
        /// The thickness of the line in pixels
        /// </summary>
        public int Thickness { get; set; }

        /// <summary>
        /// Optional. Makes line dashed if defined. Specifies the length of one dash in the line in pixels
        /// </summary>
        public int DashLength { get; set; } = 0;

        /// <summary>
        /// Optional. Only works if line is dashed. Specifies the length of the space between two dashes in pixels
        /// </summary>
        public int SpaceLength { get; set; } = 0;

        /// <summary>
        /// Creates a new instance of <see cref="LineStyle"/>
        /// </summary>
        /// <param name="thickness">The thickness of the line in pixels</param>
        /// <param name="dashLength">Optional. Makes line dashed if defined. Specifies the length of one dash in the line in pixels</param>
        /// <param name="spaceLength">Optional. Only works if line is dashed. Specifies the length of the space between two dashes in pixels</param>
        public LineStyle(int thickness, int dashLength = 0, int spaceLength = 0)
        {
            this.Thickness = thickness;
            this.DashLength = dashLength;
            this.SpaceLength = spaceLength;
        }
    }
}
