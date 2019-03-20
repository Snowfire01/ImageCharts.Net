using ImageCharts.Net.ChartProperties;
using ImageCharts.Net.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageCharts.Net.Charts
{
    public class PieChart : Chart, IChart
    {
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

        public new string GetUrlAnimated(int duration, AnimationEasing animationEasing)
        {
            var urlStringBuilder = new StringBuilder(base.GetUrlAnimated(duration, animationEasing));

            // Some software like Flowdock, Slack or Facebook messenger (and so on...) needs an URL that ends with a valid image extension file to display it as an image.
            urlStringBuilder.Append("&chof=.gif");

            return urlStringBuilder.ToString();
        }

        public new string GetUrl()
        {
            var urlStringBuilder = new StringBuilder(base.GetUrl());

            // Add inside label
            if (!string.IsNullOrWhiteSpace(this.InsideLabel))
            {
                urlStringBuilder.Append($"&chli={this.InsideLabel}");
            }

            // Add colors
            if (this.ChartData.DataSeries.Any(x => x.Fill is SingleColorFill || x.Fill is MultiColorFill))
            {
                urlStringBuilder.Append($"&chco=");

                var colorStrings = new List<string>();

                foreach (var fill in this.ChartData.DataSeries.Select(x => x.Fill))
                {
                    if (fill is SingleColorFill singleColorFill)
                    {
                        colorStrings.Add($"{singleColorFill.Color.GetHexString()}");
                    }
                    else if (fill is MultiColorFill multiColorFill)
                    {
                        colorStrings.Add($"{string.Join("|", multiColorFill.Colors.Select(x => x.Color.GetHexString()))}");
                    }
                }

                urlStringBuilder.Append(string.Join(",", colorStrings));
            }

            // Some software like Flowdock, Slack or Facebook messenger (and so on...) needs an URL that ends with a valid image extension file to display it as an image.
            urlStringBuilder.Append("&chof=.png");

            return urlStringBuilder.ToString();
        }

        protected override string GetChartTypeSpecifier()
        {
            switch (this.PieChartStyle)
            {
                case PieChartStyle.Regular2D:
                case PieChartStyle.Regular3D:
                    return "p";
                case PieChartStyle.Concentric:
                    return "pc";
                case PieChartStyle.Doughnut:
                    return "pd";
                default:
                    throw new InvalidOperationException($"{this.PieChartStyle} is not a valid pie chart style.");
            }
        }
    }
}
