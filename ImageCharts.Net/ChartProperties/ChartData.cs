using ImageCharts.Net.Enums;
using System.Collections.Generic;
using System.Linq;

namespace ImageCharts.Net.ChartProperties
{
    /// <summary>
    /// Represents the data set out of which the graph will be generated
    /// </summary>
    public class ChartData
    {
        /// <summary>
        /// Data format that will be used to encode the data in the url. Default is <see cref="DataFormat.AwesomeDataFormat"/> which is highly recommended and
        /// best suited in most, if not all, situations
        /// </summary>
        public DataFormat DataFormat { get; set; } = DataFormat.AwesomeDataFormat;

        /// <summary>
        /// The data series that make up the data
        /// </summary>
        public List<DataSeries> DataSeries { get; set; }

        /// <summary>
        /// Custom scaling. This feature works only with text-formatted values, and does not work with all chart types. 
        /// </summary>
        public List<(double Min, double Max)> CustomScalings { get; set; }

        public ChartData()
        {
            this.DataSeries = new List<DataSeries>();
        }

        public ChartData(params DataPoint[] dataPoints)
        {
            this.DataSeries = new List<DataSeries>
            {
                new DataSeries(dataPoints)
            };
        }

        public ChartData(params DataSeries[] dataSeries)
        {
            this.DataSeries = dataSeries.ToList();
        }

        public ChartData(IEnumerable<DataPoint> dataPoints)
        {
            this.DataSeries = new List<DataSeries>
            {
                 new DataSeries(dataPoints)
            };
        }

        public ChartData(IEnumerable<DataSeries> dataSeries)
        {
            this.DataSeries = DataSeries.ToList();
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
