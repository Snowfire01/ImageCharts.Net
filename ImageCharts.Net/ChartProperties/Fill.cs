using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ImageCharts.Net.ChartProperties
{
    public abstract class Fill
    {
        /// <summary>
        /// The chart area to fill.
        /// </summary>
        public Filltype Filltype { get; set; }

        /// <summary>
        /// Gets the specifier for the fill type to be used in the URL
        /// </summary>
        internal string GetFillTypeSpecifier()
        {
            switch (this.Filltype)
            {
                case Filltype.BackgroundFill:
                    return "bg";
                case Filltype.ChartAreaFill:
                    return "c";
                case Filltype.BarChartFill:
                    return "b";
                case Filltype.SliceFill:
                    return "ps";
                default:
                    return null;
            }
        }
    }
}
