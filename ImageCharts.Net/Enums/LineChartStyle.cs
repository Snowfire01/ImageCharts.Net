namespace ImageCharts.Net.Enums
{
    /// <summary>
    /// The visual style of the line chart
    /// </summary>
    public enum LineChartStyle
    {
        /// <summary>
        /// Regular line chart with x and y axes that can't be removed
        /// </summary>
        Regular,

        /// <summary>
        /// Line chart with no default axes, but the possibility to add extra ones via the chart axis property
        /// </summary>
        NoAxisLines,

        /// <summary>
        /// Lets you specify both x- and y-coordinates for each point, rather just the y values
        /// </summary>
        SpecifyCoordinates
    }
}
