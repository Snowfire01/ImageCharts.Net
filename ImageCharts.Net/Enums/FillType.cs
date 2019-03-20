namespace ImageCharts.Net.Enums
{
    /// <summary>
    /// Types of background fills that can be used for a chart
    /// </summary>
    public enum Filltype
    {
        /// <summary>
        /// Fills the whole background of the chart
        /// </summary>
        BackgroundFill,

        /// <summary>
        /// Fills only the chart area (currently same as <see cref="BackgroundFill"/>)
        /// </summary>
        ChartAreaFill,

        /// <summary>
        /// Fills the bars of a bar chart
        /// </summary>
        BarChartFill,

        /// <summary>
        /// Fills the slices of a pie chart, polar area chart or bubble chart
        /// </summary>
        SliceFill
    }
}
