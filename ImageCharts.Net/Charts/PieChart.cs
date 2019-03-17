using ImageCharts.Net.ChartProperties;
using System;
using System.Text;

namespace ImageCharts.Net.Charts
{
    public class PieChart : Chart, IChart
    {
        public PieChartStyle PieChartStyle { get; set; }

        public PieChart() : base() { }

        public PieChart(ChartData chartData, PieChartStyle pieChartStyle = PieChartStyle.Regular2D) : base(chartData)
        {
            this.PieChartStyle = pieChartStyle;
        }

        public new string GetUrl()
        {
            var urlStringBuilder = new StringBuilder(base.GetUrl());

            // Add chart width and height as url parameters
            urlStringBuilder.Append($"&cht={this.GetChartTypeSpecifier()}");

            return urlStringBuilder.ToString();
        }

        private string GetChartTypeSpecifier()
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
