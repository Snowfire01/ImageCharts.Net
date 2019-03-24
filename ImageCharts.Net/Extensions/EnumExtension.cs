using ImageCharts.Net.Enums;
using System;

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
                case ChartProperty.LineAccent:
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
                case ChartProperty.VisibleAxes:
                    return "chxt";
                case ChartProperty.AxisRange:
                    return "chxr";
                case ChartProperty.AxisLabels:
                    return "chxl";
                case ChartProperty.AxisLabelStyles:
                    return "chxs";
                case ChartProperty.GridLines:
                    return "chg";
                default:
                    throw new ArgumentException(nameof(chartProperty), "Not a valid chart property");
            }
        }

        internal static string GetUrlFormat(this ShapeMarkerType shapeMarker)
        {
            switch (shapeMarker)
            {
                case ShapeMarkerType.Circle:
                    return "o";
                case ShapeMarkerType.Cross:
                    return "X";
                case ShapeMarkerType.Diamond:
                    return "d";
                case ShapeMarkerType.Square:
                    return "s";
                case ShapeMarkerType.X:
                    return "X";
                default:
                    throw new ArgumentException(nameof(shapeMarker), "Not a valid shape marker");
            }
        }

        internal static string GetUrlFormat(this LineFillType lineFillType)
        {
            switch (lineFillType)
            {
                case LineFillType.UnderLine:
                    return "B";
                case LineFillType.BetweenLines:
                    return "b";
                default:
                    throw new ArgumentException(nameof(lineFillType), "Not a valid line fill type");
            }
        }

        internal static string GetUrlFormat(this FillType fillType)
        {
            switch (fillType)
            {
                case FillType.BackgroundFill:
                    return "bg";
                case FillType.ChartAreaFill:
                    return "c";
                default:
                    throw new ArgumentException(nameof(fillType), "Not a valid fill type");
            }
        }

        internal static string GetUrlFormat(this LineChartStyle lineChartStyle)
        {
            switch (lineChartStyle)
            {
                case LineChartStyle.Regular:
                    return "lc";
                case LineChartStyle.NoAxisLines:
                    return "ls";
                case LineChartStyle.SpecifyCoordinates:
                    return "lxy";
                default:
                    throw new ArgumentException($"{nameof(lineChartStyle)} is not a valid line chart style.");
            }
        }

        internal static string GetUrlFormat(this PieChartStyle pieChartStyle)
        {
            switch (pieChartStyle)
            {
                case PieChartStyle.Regular2D:
                case PieChartStyle.Regular3D:
                    return "p";
                case PieChartStyle.Concentric:
                    return "pc";
                case PieChartStyle.Doughnut:
                    return "pd";
                default:
                    throw new ArgumentException($"{pieChartStyle} is not a valid pie chart style.");
            }
        }

        internal static string GetUrlFormat(this BarChartStyle barChartStyle)
        {
            switch (barChartStyle)
            {
                case BarChartStyle.GroupedVertically:
                    return "bvg";
                case BarChartStyle.GroupedHorizontally:
                    return "bhg";
                case BarChartStyle.StackedVertically:
                    return "bvs";
                case BarChartStyle.StackedHorizontally:
                    return "bhs";
                default:
                    throw new ArgumentException($"{barChartStyle} is not a valid bar chart style.");
            }
        }
    }
}
