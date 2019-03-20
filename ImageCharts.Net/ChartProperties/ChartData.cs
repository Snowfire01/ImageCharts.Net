using System.Collections.Generic;
using System.Linq;

namespace ImageCharts.Net.ChartProperties
{
    public class ChartData
    {
        public DataFormat DataFormat { get; set; }

        public List<DataSeries> DataSeries { get; set; }

        public List<(double Min, double Max)> CustomScalings { get; set; }

        public ChartData(DataFormat dataFormat, List<(double min, double max)> customScalings = null)
        {
            this.DataFormat = dataFormat;
            this.DataSeries = new List<DataSeries>();
            this.CustomScalings = customScalings ?? new List<(double min, double max)>();
        }

        public ChartData(DataFormat dataFormat, List<(double min, double max)> customScalings = null, params DataPoint[] dataPoints)
        {
            this.DataFormat = dataFormat;

            this.DataSeries = new List<DataSeries>
            {
                new DataSeries
                {
                    DataPoints = dataPoints.ToList()
                }
            };

            this.CustomScalings = customScalings ?? new List<(double min, double max)>();
        }

        public ChartData(DataFormat dataFormat, List<(double min, double max)> customScalings = null, params DataSeries[] dataSeries)
        {
            this.DataFormat = dataFormat;
            this.DataSeries = dataSeries.ToList();
            this.CustomScalings = customScalings ?? new List<(double min, double max)>();
        }

        public ChartData(DataFormat dataFormat, IEnumerable<DataPoint> dataPoints, List<(double min, double max)> customScalings = null)
        {
            this.DataFormat = dataFormat;

            this.DataSeries = new List<DataSeries>
            {
                 new DataSeries
                {
                    DataPoints = dataPoints.ToList()
                }
            };

            this.CustomScalings = customScalings ?? new List<(double min, double max)>();
        }

        public ChartData(DataFormat dataFormat, IEnumerable<DataSeries> dataSeries, List<(double min, double max)> customScalings = null)
        {
            this.DataFormat = dataFormat;
            this.DataSeries = DataSeries.ToList();
            this.CustomScalings = customScalings ?? new List<(double min, double max)>();
        }

        public override string ToString()
        {
            var dataSeriesLabels = this.DataSeries
                .Where(x => !string.IsNullOrWhiteSpace(x.Label))
                .Select(x => x.Label)
                .ToList();

            return string.Join("|", dataSeriesLabels)
                .Replace(' ', '+');
        }
    }
}
