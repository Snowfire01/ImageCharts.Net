using System;
using System.Collections.Generic;
using System.Text;

namespace ImageCharts.Net.ChartProperties
{
    public class MultiColorFill : Fill
    {
        public IEnumerable<SingleColorFill> Colors { get; set; }
    }
}
