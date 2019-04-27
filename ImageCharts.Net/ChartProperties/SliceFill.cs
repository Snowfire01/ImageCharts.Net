namespace ImageCharts.Net.ChartProperties
{
    /// <summary>
    /// Lets you specify a gradient per pie slice
    /// </summary>
    public class SliceFill : GradientFill
    {
        /// <summary>
        /// The index of the series in which the targeted pie slice is contained
        /// </summary>
        public int SeriesIndex { get; set; }

        /// <summary>
        /// The index of the pie slice inside its series
        /// </summary>
        public int SliceIndex { get; set; }
    }
}
