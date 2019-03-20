using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ImageCharts.Net.ChartProperties
{
    public class GradientFill : Fill
    {
        public IEnumerable<GradientColor> GradientColors { get; set; }

        public int Angle { get; set; }
    }
}
