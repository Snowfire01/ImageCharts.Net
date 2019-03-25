namespace ImageCharts.Net.Data
{
    /// <summary>
    /// Describes a single data point on the chart
    /// </summary>
    public class DataPoint
    {
        /// <summary>
        /// The absolute value of the data point. Make null if you want a data point with no value
        /// </summary>
        public double? Value { get; set; }

        /// <summary>
        /// Optional label that will be displayed in the graph next to the data point
        /// </summary>
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// Creates a new instance of a <see cref="DataPoint"/>
        /// </summary>
        /// <param name="value">The absolute value of the data point. Make null if you want a data point with no value</param>
        /// <param name="label">Optional label that will be displayed in the graph next to the data point</param>
        public DataPoint(double? value, string label = "")
        {
            this.Value = value;
            this.Label = label;
        }
    }
}
