using ImageCharts.Net.ChartProperties;
using System.Text;

namespace ImageCharts.Net.Charts
{
    public abstract class Chart : IChart
    {
        private readonly string imagechartsBaseUrl = "https://image-charts.com/chart";

        /// <summary>
        /// The data that will be displayed in the chart
        /// </summary>
        public ChartData ChartData { get; set; }

        /// <summary>
        /// Width of the generated chart in pixels. Values higher than 999 will result in an error when retrieving the chart from the url.
        /// </summary>
        public int ChartWidth { get; set; }

        /// <summary>
        /// Height of the generated chart in pixels. Values higher than 999 will result in an error when retrieving the chart from the url.
        /// </summary>
        public int ChartHeight { get; set; }

        /// <summary>
        /// Title properties of the generated chart
        /// </summary>
        public ChartTitle ChartTitle { get; set; }

        /// <summary>
        /// The margin of the chart inrespect to the borders of the image
        /// </summary>
        public (int LeftMargin, int RightMargin, int TopMargin, int BottomMargin) Margin { get; set; }

        public Chart()
        {
            this.ChartData = new ChartData(DataFormat.AwesomeDataFormat);
        }

        public Chart(ChartData chartData)
        {
            this.ChartData = chartData;
        }

        /// <summary>
        /// Gets the url that can be used to retrieve a plotted chart from the imagecharts service with the current properties of the chart object
        /// </summary>
        public string GetUrl()
        {
            var urlStringBuilder = new StringBuilder(this.imagechartsBaseUrl);

            // Add chart width and height as url parameters
            urlStringBuilder.Append($"?chs={this.ChartWidth}x{this.ChartHeight}");

            // Add chart title and its properties as url parameters
            urlStringBuilder.Append($"&chtt={this.ChartTitle}&chts={this.ChartTitle.GetTextColorString()},{this.ChartTitle.FontSize}");

            // Add chart data as url parameter
            urlStringBuilder.Append($"&chd={ChartDataEncoder.GetFormatSpecifier(this.ChartData)}:{ChartDataEncoder.GetEncodedValues(this.ChartData)}");

            // Add scaling for chart data (if necessary) as url parameter
            if (this.ChartData.DataFormat == DataFormat.TextFormatAutomaticScaling || this.ChartData.DataFormat == DataFormat.TextFormatCustomScaling)
                urlStringBuilder.Append($"$chds={ChartDataEncoder.GetScalingSpecifier(this.ChartData)}");

            // Add labels for data series as url parameter
            urlStringBuilder.Append($"&chdl={this.ChartData.GetDataSeriesLabelString()}");

            // Add chart margin as url parameter
            urlStringBuilder.Append($"&chma={this.Margin.LeftMargin},{this.Margin.RightMargin},{this.Margin.TopMargin},{this.Margin.BottomMargin}");

            return urlStringBuilder.ToString();
        }
    }
}
