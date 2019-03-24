using System.Collections.Generic;

namespace ImageCharts.Net.ChartProperties
{
    /// <summary>
    /// Describes a multi-color fill with multiple colors that will be displayed varyingly
    /// </summary>
    public class MultiColorFill : Fill
    {
        /// <summary>
        /// The colors of the fill
        /// </summary>
        public IEnumerable<SingleColorFill> Colors { get; set; }
    }
}
