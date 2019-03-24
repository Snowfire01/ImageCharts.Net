using System;
using System.Collections.Generic;
using System.Text;

namespace ImageCharts.Net.Enums
{
    public enum LineFillType
    {
        /// <summary>
        /// Fill all the space between the line and the base of the chart
        /// </summary>
        UnderLine,

        /// <summary>
        /// Fill all the space between two lines. Not yet supported by imagecharts and won't yield any results.
        /// </summary>
        BetweenLines
    }
}
