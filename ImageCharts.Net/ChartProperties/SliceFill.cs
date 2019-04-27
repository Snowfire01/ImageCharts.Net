using System.Collections.Generic;

namespace ImageCharts.Net.ChartProperties
{
    public class SliceFill
    {
        public int SeriesIndex { get; set; }

        public int SliceIndex { get; set; }

        public IEnumerable<GradientColor> GradientColors { get; set; }
    }
}
