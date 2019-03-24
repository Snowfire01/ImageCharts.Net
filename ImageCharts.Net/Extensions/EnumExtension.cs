using System;
using System.Collections.Generic;
using System.Text;

namespace ImageCharts.Net.Extensions
{
    internal static class EnumExtension
    {
        internal static string GetUrlFormat(this ChartProperty chartProperty)
        {
            switch (chartProperty)
            {
                case ChartProperty.ChartType:
                    return "cht";
                case ChartProperty.Data:
                    return "chd";
                case ChartProperty.DataFill:
                    return "chco";
                case ChartProperty.DataPointLabels:
                    return "chl";
                case ChartProperty.DataSeriesLabels:
                    return "chdl";
                case ChartProperty.Fill:
                    return "chf";
                case ChartProperty.InsideLabel:
                    return "chli";
                case ChartProperty.LineFill:
                    return "chm";
                case ChartProperty.LineStyle:
                    return "chls";
                case ChartProperty.Margin:
                    return "chma";
                case ChartProperty.OutputFormat:
                    return "chof";
                case ChartProperty.Scaling:
                    return "chds";
                case ChartProperty.Size:
                    return "chs";
                case ChartProperty.Title:
                    return "chtt";
                case ChartProperty.Animation:
                    return "chan";
                default:
                    throw new ArgumentException(nameof(chartProperty), "Not a valid chart property");
            }
        }
    }
}
