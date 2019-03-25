using ImageCharts.Net.ChartProperties;
using ImageCharts.Net.Data;
using ImageCharts.Net.Enums;
using ImageCharts.Net.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace ImageCharts.Net.Charts
{
    public class PieChart : Chart, IChart
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
            if (this.ChartData.DataSeries.Any(x => x.Fill is SingleColorFill || x.Fill is MultiColorFill))
            {
                chartProperties.Add(ChartProperty.DataFill, string.Empty);

                var colorStrings = new List<string>();

                foreach (var fill in this.ChartData.DataSeries.Select(x => x.Fill))
                {
                    if (fill is SingleColorFill singleColorFill)
                    {
                        colorStrings.Add(singleColorFill.Color.GetHexString());
                    }
                    else if (fill is MultiColorFill multiColorFill)
                    {
                        colorStrings.Add($"{string.Join("|", multiColorFill.Colors.Select(x => x.Color.GetHexString()))}");
                    }
                }

                chartProperties[ChartProperty.DataFill] = string.Join(",", colorStrings);
            }

            return chartProperties;
        }

        protected override string GetChartTypeSpecifier() => this.PieChartStyle.GetUrlFormat();
    }
}
