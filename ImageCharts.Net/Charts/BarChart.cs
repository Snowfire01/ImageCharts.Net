using ImageCharts.Net.ChartProperties;
using ImageCharts.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageCharts.Net.Charts
{
    public class BarChart : Chart
    {
        public BarChartStyle BarChartStyle { get; set; }

        public BarChart() : base() { }

        public BarChart(ChartData chartData, BarChartStyle lineChartStyle) : base(chartData)
        {
            this.BarChartStyle = BarChartStyle;
        }

        protected override string GetChartTypeSpecifier()
        {
            switch (this.BarChartStyle)
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
                    throw new InvalidOperationException($"{this.BarChartStyle} is not a valid bar chart style.");
            }
        }
    }
}
