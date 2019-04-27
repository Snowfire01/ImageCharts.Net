using ImageCharts.Net.ChartProperties;
using ImageCharts.Net.Data;
using ImageCharts.Net.Enums;
using ImageCharts.Net.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace ImageCharts.Net.Charts
{
    public class PieChart : Chart
    {
        /// <summary>
        /// The visual display style of the pie chart
        /// </summary>
        public PieChartStyle PieChartStyle { get; set; }

        /// <summary>
        /// Gets or Sets the inside label of the pie chart (only applies for doughnut charts)
        /// </summary>
        public string InsideLabel { get; set; }

        public PieChart() : base() { }

        public PieChart(ChartData chartData, PieChartStyle pieChartStyle = PieChartStyle.Regular2D) : base(chartData)
        {
            this.PieChartStyle = pieChartStyle;
        }

        protected override Dictionary<ChartProperty, string> GetChartProperties()
        {
            var chartProperties = base.GetChartProperties();

            // Add inside label
            if (!string.IsNullOrWhiteSpace(this.InsideLabel))
            {
                chartProperties.Add(ChartProperty.InsideLabel, this.InsideLabel);
            }

            // Add colors
            if (this.ChartData.DataSeries.Any(x => x.Fill != null))
            {
                var colors = this.ChartData.DataSeries.Select(x => x.Fill).ToList();

                var colorStrings = new List<string>();
                var gradientStrings = new List<string>();

                foreach (var fill in colors)
                {
                    if (fill is SingleColorFill singleColorFill)
                    {
                        colorStrings.Add(singleColorFill.Color.GetHexString());
                    }
                    else if (fill is MultiColorFill multiColorFill)
                    {
                        colorStrings.Add($"{string.Join("|", multiColorFill.Colors.Select(x => x.Color.GetHexString()))}");
                    }
                    else if (fill is SliceFill sliceFill)
                    {
                        colorStrings.Add($"s{sliceFill.SeriesIndex}-{sliceFill.SliceIndex},lg,{sliceFill.Angle}," +
                            $"{string.Join(",", sliceFill.GradientColors.Select(x => $"{x.Color.GetHexString()},{x.CenterPoint}"))}");
                    }
                    else if (fill is GradientFill gradientFill)
                    {
                        var dataSeriesIndex = colors.IndexOf(fill);

                        gradientStrings.Add($"b{dataSeriesIndex},lg,{gradientFill.Angle}," +
                            $"{string.Join(",", gradientFill.GradientColors.Select(x => $"{x.Color.GetHexString()},{x.CenterPoint}"))}");
                    }
                }

                if (colorStrings.Any())
                {
                    chartProperties.Add(ChartProperty.DataFill, string.Empty);

                    chartProperties[ChartProperty.DataFill] = string.Join(",", colorStrings);
                }

                if (gradientStrings.Any())
                {
                    if (!chartProperties.ContainsKey(ChartProperty.Fill))
                    {
                        chartProperties.Add(ChartProperty.Fill, string.Join("|", gradientStrings));
                    }
                    else
                    {
                        chartProperties[ChartProperty.Fill] += $"|{string.Join("|", gradientStrings)}";
                    }
                }
            }

            return chartProperties;
        }

        protected override string GetChartTypeSpecifier() => this.PieChartStyle.GetUrlFormat();
    }
}
