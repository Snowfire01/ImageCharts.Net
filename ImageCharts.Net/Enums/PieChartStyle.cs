namespace ImageCharts.Net.Enums
{
    /// <summary>
    /// The visual style of the pie chart
    /// </summary>
    public enum PieChartStyle
    {
        /// <summary>
        /// Regular 2 dimensional pie chart. If multiple series are specified, automatically falls back to <see cref="Concentric"/>
        /// </summary>
        Regular2D,

        /// <summary>
        /// Not yet supported, falls back to <see cref="Regular2D"/>
        /// </summary>
        Regular3D,

        /// <summary>
        /// Use this type if you have multiple data series to display. Displays them as rings around each other.
        /// </summary>
        Concentric,

        /// <summary>
        /// Pie with space for a label in the center. Can be used ti display multiple data series.
        /// </summary>
        Doughnut
    }
}
