using ImageCharts.Net.ChartProperties;
using ImageCharts.Net.Data;
using ImageCharts.Net.Enums;
using ImageCharts.Net.Extensions;
using ImageCharts.Net.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageCharts.Net.Charts
{
    public abstract class Chart
    {
        protected const string imagechartsBaseUrl = "https://image-charts.com/chart?";

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
        public IEnumerable<string> LegendItems { get; set; } = new List<string>();

        /// <summary>
        /// The background fill of the chart. Can be either a <see cref="SingleColorFill"/> for single-color-backgrounds or a <see cref="GradientFill"/> for gradient backgrounds
        /// </summary>
        public Fill Fill { get; set; }

        public Chart()
        {
            this.ChartData = new ChartData();
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
        public string GetUrlAnimated(int duration, AnimationEasing easing)
        {
            var chartProperties = this.GetChartProperties();

            chartProperties.Add(ChartProperty.Animation, $"{duration},{char.ToLower(easing.ToString()[0])}{easing.ToString().Substring(1)}");

            chartProperties[ChartProperty.OutputFormat] = ".gif";

            // Order chart properties so that the output format is at the end (otherwise a file extension wouldn't make any sense at all)
            var returnValue =  chartProperties.OrderBy(x => x.Key == ChartProperty.OutputFormat);

            var urlBuilder = new StringBuilder(imagechartsBaseUrl);

            foreach (var chartProperty in returnValue)
            {
                urlBuilder.Append($"&{chartProperty.Key.GetUrlFormat()}={chartProperty.Value}");
            }

            return urlBuilder.ToString();
        }

        /// <summary>
        /// Gets a url that can be used to retrieve a plotted chart from the imagecharts service with the current properties of the chart object
        /// </summary>
        public string GetUrl()
        {
            // Order chart properties so that the output format is at the end (otherwise a file extension wouldn't make any sense at all)
            var chartProperties = this.GetChartProperties().OrderBy(x => x.Key == ChartProperty.OutputFormat);

            var urlBuilder = new StringBuilder(imagechartsBaseUrl);

            foreach (var chartProperty in chartProperties)
            {
                urlBuilder.Append($"&{chartProperty.Key.GetUrlFormat()}={chartProperty.Value}");
            }

            return urlBuilder.ToString();
        }

        protected virtual Dictionary<ChartProperty, string> GetChartProperties()
        {
            var chartProperties = new Dictionary<ChartProperty, string>();

            // Add chart width and height as url parameters
            chartProperties.Add(ChartProperty.ChartType, this.GetChartTypeSpecifier());

            // Add chart width and height as url parameters
            chartProperties.Add(ChartProperty.Size, $"{this.ChartWidth}x{this.ChartHeight}");

            // Add chart title and its properties as url parameters
            chartProperties.Add(ChartProperty.TitleText, $"{this.ChartTitle.Text}");
            chartProperties.Add(ChartProperty.TitleFont, $"{this.ChartTitle.TextColor.GetHexString()},{this.ChartTitle.FontSize}");

            // Add chart data as url parameter
            chartProperties.Add(ChartProperty.Data, $"{ChartDataEncoder.GetFormatSpecifier(this.ChartData)}:{ChartDataEncoder.GetEncodedValues(this.ChartData)}");

            // Add scaling for chart data (if necessary) as url parameter
            if (this.ChartData.DataFormat == DataFormat.TextFormatAutomaticScaling || this.ChartData.DataFormat == DataFormat.TextFormatCustomScaling)
            {
                chartProperties.Add(ChartProperty.Scaling, $"{ChartDataEncoder.GetScalingSpecifier(this.ChartData)}");
            }

            // Add labels for data series as url parameter
            if (this.LegendItems.Any())
            {
                chartProperties.Add(ChartProperty.DataSeriesLabels, $"{string.Join("|", this.LegendItems)}");
            }

            // Add labels for each data point
            if (this.ChartData.GetDataPoints().Any(x => !string.IsNullOrWhiteSpace(x.Label)))
            {
                chartProperties.Add(ChartProperty.DataPointLabels, $"{string.Join("|", this.ChartData.GetDataPoints().Select(x => x.Label))}");
            }

            // Add chart margin as url parameter
            chartProperties.Add(ChartProperty.Margin, $"{this.Margin.MarginLeft},{this.Margin.MarginRight},{this.Margin.MarginTop},{this.Margin.MarginBottom}");

            if (this.Fill != null)
            {
                chartProperties.Add(ChartProperty.Fill, string.Empty);

                if (this.Fill is SingleColorFill colorFill)
                {
                    chartProperties[ChartProperty.Fill] = $"{this.Fill.FillType.GetUrlFormat()},s,{colorFill.Color.GetHexString()}";
                }
                else if (this.Fill is GradientFill gradientFill)
                {
                    chartProperties[ChartProperty.Fill] = $"{this.Fill.FillType.GetUrlFormat()},lg,{gradientFill.Angle}," +
                        $"{string.Join(",", gradientFill.GradientColors.Select(x => $"{x.Color.GetHexString()},{x.CenterPoint}"))}";
                }
            }

            // Some software like Flowdock, Slack or Facebook messenger (and so on...) needs an URL that ends with a valid image extension file to
            // display it as an image.
            chartProperties.Add(ChartProperty.OutputFormat, ".png");

            return chartProperties;
        }

        protected abstract string GetChartTypeSpecifier();
    }
}
