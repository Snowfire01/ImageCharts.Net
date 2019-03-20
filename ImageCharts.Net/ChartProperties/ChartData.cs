using ImageCharts.Net.Enums;
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

        /// <summary>
        /// Gets all data points of the data set as a single enumerable instead of encapsulated in their respective data series
        /// </summary>
        /// <returns></returns>
        public List<DataPoint> GetDataPoints()
        {
            var returnValue = new List<DataPoint>();

            foreach (var dataSeries in this.DataSeries)
            {
                returnValue.AddRange(dataSeries.DataPoints);
            }

            return returnValue;
        }
    }
}
