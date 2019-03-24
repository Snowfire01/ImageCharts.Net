using ImageCharts.Net.ChartProperties;
using ImageCharts.Net.Enums;
using ImageCharts.Net.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageCharts.Net.Charts
{
    public class LineChart : Chart
    {
        public LineChartStyle LineChartStyle { get; set; }

        public GridLines? GridLines { get; set; }

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

        protected override Dictionary<ChartProperty, string> GetChartProperties()
        {
            var chartProperties = base.GetChartProperties();

            // Add colors
            if (this.ChartData.DataSeries.Any())
            {
                chartProperties.Add(ChartProperty.DataFill, string.Empty);

                var colorStrings = new List<string>();

                foreach (var fill in this.ChartData.DataSeries.Select(x => x.Fill))
                {
                    if (fill is SingleColorFill singleColorFill)
                    {
                        colorStrings.Add(singleColorFill.Color.GetHexString());
                    }
                }

                chartProperties[ChartProperty.DataFill] = string.Join(",", colorStrings);
            }

            // Add line style
            if (this.ChartData.DataSeries.Any(x => x.LineStyle.HasValue))
            {
                chartProperties.Add(ChartProperty.LineStyle, string.Empty);

                var lineStyleStrings = new List<string>();

                foreach (var dataSeries in this.ChartData.DataSeries)
                {
                    if (dataSeries.LineStyle.HasValue)
                    {
                        lineStyleStrings.Add($"{dataSeries.LineStyle.Value.Thickness},{dataSeries.LineStyle.Value.DashLength}," +
                            $"{dataSeries.LineStyle.Value.SpaceLength}");
                    }
                    else
                    {
                        lineStyleStrings.Add("1,0,0");
                    }
                }

                chartProperties[ChartProperty.LineStyle] = string.Join("|", lineStyleStrings);
            }

            // Add line fill
            if (this.ChartData.DataSeries.Any(x => x.LineFill.HasValue))
            {
                chartProperties.Add(ChartProperty.LineAccent, string.Empty);

                var lineFillStrings = new List<string>();

                foreach (var dataSeries in this.ChartData.DataSeries)
                {
                    if (dataSeries.LineFill.HasValue)
                    {
                        var lineFill = dataSeries.LineFill.Value;
                        var lineFillTypeString = lineFill.LineFillType == LineFillType.UnderLine ? "B" : "b";
                        var lineFillEndLineString = lineFill.LineFillType == LineFillType.UnderLine ?
                            $"{lineFill.FillStartStop.start}:{lineFill.FillStartStop.end}" : $"{lineFill.EndLineIndex}";

                        lineFillStrings.Add($"{lineFillTypeString},{lineFill.Color.GetHexString()},{this.ChartData.DataSeries.IndexOf(dataSeries)},{lineFillEndLineString}");
                    }
                }

                chartProperties[ChartProperty.LineAccent] = string.Join("|", lineFillStrings);
            }

            // Add shape markers
            if (this.ChartData.DataSeries.Any(x => x.ShapeMarker.HasValue))
            {
                var shapeMarkerStrings = new List<string>();

                foreach (var dataSeries in this.ChartData.DataSeries)
                {
                    if (dataSeries.ShapeMarker.HasValue)
                    {
                        var shapeMarker = dataSeries.ShapeMarker.Value;

                        shapeMarkerStrings.Add($"{shapeMarker.ShapeMarkerType.GetUrlFormat()},{shapeMarker.Color.GetHexString()}," +
                            $"{this.ChartData.DataSeries.IndexOf(dataSeries)},-1,{shapeMarker.Size}");
                    }
                }

                if (!chartProperties.ContainsKey(ChartProperty.LineAccent))
                {
                    chartProperties.Add(ChartProperty.LineAccent, string.Empty);
                    chartProperties[ChartProperty.LineAccent] = string.Join("|", shapeMarkerStrings);
                }
                else
                {
                    chartProperties[ChartProperty.LineAccent] += $"|{string.Join("|", shapeMarkerStrings)}";
                }
            }

            // Add grid lines
            if (this.GridLines.HasValue)
            {
                var gridLines = this.GridLines.Value;

                chartProperties.Add(ChartProperty.GridLines,
                    $"{Convert.ToInt32(gridLines.ShowHorizontalGridLines)}," +
                    $"{Convert.ToInt32(gridLines.ShowVerticalGridLines)}," +
                    $"{gridLines.DashLength ?? 4}," +
                    $"{gridLines.SpaceLength ?? 1}");
            }

            return chartProperties;
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
