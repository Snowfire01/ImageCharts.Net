using System.Collections.Generic;

namespace ImageCharts.Net.ChartProperties
{
    /// <summary>
    /// Describes a gradient fill with any number of gradient colors that fade into each other
    /// </summary>
    public class GradientFill : Fill
    {
        /// <summary>
        /// The gradient colors that fade into each other in the provided order 
        /// </summary>
        public IEnumerable<GradientColor> GradientColors { get; set; }

        /// <summary>
        /// Value between 0 and 90. The gradient angle. 0 => left to right. 45 => top left corner to bottom right corner. 90 => top to bottom.
        /// </summary>
        public int Angle { get; set; }
    }
}
