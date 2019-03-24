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

        public GridLines GridLines { get; set; }

        public LineChart() : base() { }

        public LineChart(ChartData chartData, LineChartStyle lineChartStyle = LineChartStyle.Regular) : base(chartData)
        {
            this.LineChartStyle = lineChartStyle;
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
            if (this.ChartData.DataSeries.Any(x => x.LineStyle != null))
            {
                chartProperties.Add(ChartProperty.LineStyle, string.Empty);

                var lineStyleStrings = new List<string>();

                foreach (var dataSeries in this.ChartData.DataSeries)
                {
                    if (dataSeries.LineStyle != null)
                    {
                        lineStyleStrings.Add($"{dataSeries.LineStyle.Thickness},{dataSeries.LineStyle.DashLength},{dataSeries.LineStyle.SpaceLength}");
                    }
                    else
                    {
                        lineStyleStrings.Add("1,0,0");
                    }
                }

                chartProperties[ChartProperty.LineStyle] = string.Join("|", lineStyleStrings);
            }

            // Add line fill
            if (this.ChartData.DataSeries.Any(x => x.LineFill != null))
            {
                chartProperties.Add(ChartProperty.LineAccent, string.Empty);

                var lineFillStrings = new List<string>();

                foreach (var dataSeries in this.ChartData.DataSeries)
                {
                    if (dataSeries.LineFill != null)
                    {
                        var lineFill = dataSeries.LineFill;
                        var lineFillTypeString = lineFill.LineFillType == LineFillType.UnderLine ? "B" : "b";

                        lineFillStrings.Add($"{lineFillTypeString},{lineFill.Color.GetHexString()},{this.ChartData.DataSeries.IndexOf(dataSeries)},0");
                    }
                }

                chartProperties[ChartProperty.LineAccent] = string.Join("|", lineFillStrings);
            }

            // Add shape markers
            if (this.ChartData.DataSeries.Any(x => x.ShapeMarker == null))
            {
                var shapeMarkerStrings = new List<string>();

                foreach (var dataSeries in this.ChartData.DataSeries)
                {
                    if (dataSeries.ShapeMarker != null)
                    {
                        var shapeMarker = dataSeries.ShapeMarker;

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
            if (this.GridLines != null)
            {
                chartProperties.Add(ChartProperty.GridLines,
                    $"{Convert.ToInt32(this.GridLines.ShowHorizontalGridLines)}," +
                    $"{Convert.ToInt32(this.GridLines.ShowVerticalGridLines)}," +
                    $"{this.GridLines.DashLength}," +
                    $"{this.GridLines.SpaceLength}");
            }

            return chartProperties;
        }

        protected override string GetChartTypeSpecifier() => this.LineChartStyle.GetUrlFormat();
    }
}
