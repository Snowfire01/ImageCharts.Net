using ImageCharts.Net.ChartProperties;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImageCharts.Net
{
    public class DataSeries
    {
        public List<DataPoint> DataPoints { get; set; }

        public Fill Fill { get; set; }

        public LineStyle? LineStyle { get; set; }

        public LineFill? LineFill { get; set; }

        public ShapeMarker? ShapeMarker { get; set; }

        public DataSeries(IEnumerable<DataPoint> dataPoints = null)
        {
            this.DataPoints = dataPoints?.ToList() ?? new List<DataPoint>();
        }
    }
}
