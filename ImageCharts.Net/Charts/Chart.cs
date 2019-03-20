using ImageCharts.Net.ChartProperties;
using System.Collections.Generic;
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
        public (int MarginLeft, int MarginRight, int MarginTop, int MarginBottom) Margin { get; set; }

        /// <summary>
        /// Collection of strings that will be displayed as legend items
        /// </summary>
        public IEnumerable<string> LegendItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Fill Fill { get; set; }

        public Chart()
        {
            this.ChartData = new ChartData(DataFormat.AwesomeDataFormat);
        }

        public Chart(ChartData chartData)
        {
            this.ChartData = chartData;
        }

        /// <summary>
        /// Gets a url that can be used to retrieve a plotted chart from the imagecharts service with the current properties of the chart object
        /// that is an animated gif.
        /// </summary>
        /// <param name="duration">The duration of the gif in milliesconds (maximum is 1500)</param>
        /// <param name="easing">The easing type of the animation.</param>
        public string GetUrlAnimated(int duration, AnimationEasing easing) =>
            $"{this.GetUrl()}&chan={duration},{char.ToLower(easing.ToString()[0])}{easing.ToString().Substring(1)}&chof=.gif";

        /// <summary>
        /// Gets a url that can be used to retrieve a plotted chart from the imagecharts service with the current properties of the chart object
        /// </summary>
        public string GetUrl()
        {
            var urlStringBuilder = new StringBuilder(this.imagechartsBaseUrl);

            // Add chart width and height as url parameters
            urlStringBuilder.Append($"?chs={this.ChartWidth}x{this.ChartHeight}");

            // Add chart title and its properties as url parameters
            urlStringBuilder.Append($"&chtt={this.ChartTitle}&chts={this.ChartTitle.TextColor.GetHexString()},{this.ChartTitle.FontSize}");

            // Add chart data as url parameter
            urlStringBuilder.Append($"&chd={ChartDataEncoder.GetFormatSpecifier(this.ChartData)}:{ChartDataEncoder.GetEncodedValues(this.ChartData)}");

            // Add scaling for chart data (if necessary) as url parameter
            if (this.ChartData.DataFormat == DataFormat.TextFormatAutomaticScaling || this.ChartData.DataFormat == DataFormat.TextFormatCustomScaling)
            {
                urlStringBuilder.Append($"$chds={ChartDataEncoder.GetScalingSpecifier(this.ChartData)}");
            }

            // Add labels for data series as url parameter
            urlStringBuilder.Append($"&chdl={string.Join("|", this.LegendItems)}");

            // Add chart margin as url parameter
            urlStringBuilder.Append($"&chma={this.Margin.MarginLeft},{this.Margin.MarginRight},{this.Margin.MarginTop},{this.Margin.MarginBottom}");

            urlStringBuilder.Append("&chf=");

            if (this.Fill is ColorFill colorFill)
            {
                urlStringBuilder.Append($"{this.Fill.GetFillTypeSpecifier()},s,{colorFill.Color.GetHexString()}");
            }
            else if (this.Fill is GradientFill gradientFill)
            {
                urlStringBuilder.Append($"{this.Fill.GetFillTypeSpecifier()},lg,{gradientFill.FirstColor.CenterPoint},{gradientFill.FirstColor.Color.GetHexString()}," +
                    $"{gradientFill.SecondColor.CenterPoint},{gradientFill.SecondColor.Color.GetHexString()}");
            }

            // Some software like Flowdock, Slack or Facebook messenger (and so on...) needs an URL that ends with a valid image extension file to display it as an image.
            urlStringBuilder.Append("$chof=.png");

            return urlStringBuilder.ToString();
        }
    }
}
