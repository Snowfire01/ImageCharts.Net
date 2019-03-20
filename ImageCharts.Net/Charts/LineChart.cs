using ImageCharts.Net.ChartProperties;
using ImageCharts.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageCharts.Net.Charts
{
    public class LineChart : Chart
    {
        public LineChartStyle LineChartStyle { get; set; }

        public LineChart() : base() { }

        public LineChart(ChartData chartData, LineChartStyle lineChartStyle = LineChartStyle.Regular) : base(chartData)
        {
            this.LineChartStyle = lineChartStyle;
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

            // Add colors
            if (this.ChartData.DataSeries.Any())
            {
                urlStringBuilder.Append($"&chco=");

                foreach (var fill in this.ChartData.DataSeries.Select(x => x.Fill))
                {
                    if (fill is SingleColorFill singleColorFill)
                    {
                        urlStringBuilder.Append($"{singleColorFill.Color.GetHexString()},");
                    }
                }
            }

            // Add line style
            if (this.ChartData.DataSeries.Any(x => x.LineStyle.HasValue))
            {
                urlStringBuilder.Append($"&chls=");

                var lineStyleStrings = new List<string>();

                foreach (var dataSeries in this.ChartData.DataSeries)
                {
                    if (dataSeries.LineStyle.HasValue)
                    {
                        lineStyleStrings.Add($"{dataSeries.LineStyle.Value.Thickness},{dataSeries.LineStyle.Value.DashLength},{dataSeries.LineStyle.Value.SpaceLength}");
                    }
                    else
                    {
                        lineStyleStrings.Add("1,0,0");
                    }
                }

                urlStringBuilder.Append(string.Join("|", lineStyleStrings));
            }

            // Add line fill
            if (this.ChartData.DataSeries.Any(x => x.LineFill.HasValue))
            {
                urlStringBuilder.Append($"&chm=");

                var lineStyleStrings = new List<string>();

                foreach (var dataSeries in this.ChartData.DataSeries)
                {
                    if (dataSeries.LineFill.HasValue)
                    {
                        var lineFill = dataSeries.LineFill.Value;
                        var lineFillTypeString = lineFill.LineFillType == LineFillType.UnderLine ? "B" : "b";
                        var lineFillEndLineString = lineFill.LineFillType == LineFillType.UnderLine ? 
                            $"{lineFill.FillStartStop.start}:{lineFill.FillStartStop.end}" : $"{lineFill.EndLineIndex}";

                        lineStyleStrings.Add($"{lineFillTypeString},{lineFill.Color.GetHexString()},{this.ChartData.DataSeries.IndexOf(dataSeries)},{lineFillEndLineString}");
                    }
                }

                urlStringBuilder.Append(string.Join("|", lineStyleStrings));
            }

            // Some software like Flowdock, Slack or Facebook messenger (and so on...) needs an URL that ends with a valid image extension file to display it as an image.
            urlStringBuilder.Append("&chof=.png");

            return urlStringBuilder.ToString();
        }

        protected override string GetChartTypeSpecifier()
        {
            switch (this.LineChartStyle)
            {
                case LineChartStyle.Regular:
                    return "lc";
                case LineChartStyle.NoAxisLines:
                    return "ls";
                case LineChartStyle.SpecifyCoordinates:
                    return "lxy";
                default:
                    throw new InvalidOperationException($"{this.LineChartStyle} is not a valid line chart style.");
            }
        }
    }
}
