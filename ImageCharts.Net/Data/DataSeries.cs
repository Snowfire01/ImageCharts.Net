using ImageCharts.Net.ChartProperties;
using System.Collections.Generic;
using System.Linq;

namespace ImageCharts.Net.Data
{
    /// <summary>
    /// Contains a series of data points and associated properties.
    /// </summary>
    public class DataSeries
    {
        /// <summary>
        /// The data points that will be represented by this data series
        /// </summary>
        public List<DataPoint> DataPoints { get; set; }

        /// <summary>
        /// Optional fill that can be used to describe in which colors the series will be presented in the graph
        /// </summary>
        public Fill Fill { get; set; }

        /// <summary>
        /// Optional line style that can be used to describe the properties of the displayed line
        /// </summary>
        public LineStyle LineStyle { get; set; }

        /// <summary>
        /// Optional line fill that can be used to describe a color fill below the displayed line
        /// </summary>
        public LineFill LineFill { get; set; }

        /// <summary>
        /// Optional shape markers that can be used to describe shape markers displayed at the data points
        /// </summary>
        public ShapeMarker ShapeMarker { get; set; }

        /// <summary>
        /// Creates a new instance of a <see cref="DataSeries"/>
        /// </summary>
        /// <param name="dataPoints">The data points that will be represented by this data series</param>
        /// <param name="fill">Optional fill that can be used to describe in which colors the series will be presented in the graph</param>
        /// <param name="lineStyle">Optional line style that can be used to describe the properties of the displayed line</param>
        /// <param name="lineFill">Optional line fill that can be used to describe a color fill below the displayed line</param>
        /// <param name="shapeMarker">Optional shape markers that can be used to describe shape markers displayed at the data points</param>
        public DataSeries(IEnumerable<DataPoint> dataPoints, Fill fill = null, LineStyle lineStyle = null, LineFill lineFill = null, ShapeMarker shapeMarker = null)
        {
            this.DataPoints = dataPoints?.ToList() ?? new List<DataPoint>();
            this.Fill = fill;
            this.LineStyle = lineStyle;
            this.LineFill = lineFill;
            this.ShapeMarker = shapeMarker;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="DataSeries"/>
        /// </summary>
        /// <param name="dataPoints">The data points that will be represented by this data series</param>
        public DataSeries(params DataPoint[] dataPoints)
        {
            this.DataPoints = dataPoints?.ToList() ?? new List<DataPoint>();
            this.Fill = null;
            this.LineStyle = null;
            this.LineFill = null;
            this.ShapeMarker = null;
        }
    }
}
