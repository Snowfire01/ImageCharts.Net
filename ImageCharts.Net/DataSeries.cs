using System.Collections.Generic;
using System.Linq;

namespace ImageCharts.Net
{
    public class DataSeries
    {
        public List<DataPoint> DataPoints { get; set; }

        public string Label { get; set; }

        public DataSeries(IEnumerable<DataPoint> dataPoints = null, string label = "")
        {
            this.DataPoints = dataPoints?.ToList() ?? new List<DataPoint>();
            this.Label = label;
        }
    }
}
