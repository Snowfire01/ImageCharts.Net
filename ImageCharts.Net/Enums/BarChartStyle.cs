namespace ImageCharts.Net.Enums
{
    /// <summary>
    /// the visual style of the bar chart
    /// </summary>
    public enum BarChartStyle
    {
        /// <summary>
        /// Bars go from bottom to top. Multiple data series are displayed next to each other.
        /// </summary>
        GroupedVertically,

        /// <summary>
        /// Bars go from left to right. Multiple data series are displayed next to each other.
        /// </summary>
        GroupedHorizontally,

        /// <summary>
        /// Bars go from bottom to top. Multiple data series are displayed on top of each other.
        /// </summary>
        StackedVertically,

        /// <summary>
        /// Bars go from left to right. Multiple data series are displayed on top of each other.
        /// </summary>
        StackedHorizontally,
    }
}
