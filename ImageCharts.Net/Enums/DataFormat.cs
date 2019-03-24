namespace ImageCharts.Net.Enums
{
    /// <summary>
    /// Data format that will be used to encode the data in the url
    /// </summary>
    public enum DataFormat
    {
        /// <summary>
        /// RECOMMENDED
        /// </summary>
        AwesomeDataFormat,

        /// <summary>
        /// Allows floating point numbers from 0—100
        /// </summary>
        BasicTextFormat,

        /// <summary>
        /// Scales the chart to fit your data.
        /// </summary>
        TextFormatAutomaticScaling,

        /// <summary>
        /// Similar to <see cref="BasicTextFormat"/>, but it lets you specify a custom range using a second URL parameter
        /// </summary>
        TextFormatCustomScaling,

        /// <summary>
        /// Value range from 0-61, encoded by single alphanumeric char. Produces shorter urls than other formats.
        /// </summary>
        SimpleEncodingFormat,

        /// <summary>
        /// Value range from 0-4095, encoded by two alphanumeric chars. Produces shorter urls than other formats.
        /// </summary>
        ExtendedEncodingFormat
    }
}
