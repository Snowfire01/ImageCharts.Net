﻿using ImageCharts.Net.ChartProperties;
using ImageCharts.Net.Data;
using ImageCharts.Net.Enums;
using ImageCharts.Net.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageCharts.Net.Charts
{
    public class PolarChart : Chart
    {
        public GridLines GridLines { get; set; }

        public PolarChart() : base() { }

        public PolarChart(ChartData chartData) : base(chartData) { }

        protected override Dictionary<ChartProperty, string> GetChartProperties()
        {
            var chartProperties = base.GetChartProperties();

            // Add colors
            if (this.ChartData.DataSeries.Any(x => x.Fill is SingleColorFill))
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

        protected override string GetChartTypeSpecifier() => "pa";
    }
}
