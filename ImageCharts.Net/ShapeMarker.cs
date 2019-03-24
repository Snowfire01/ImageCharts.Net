using ImageCharts.Net.Enums;
using System.Drawing;

namespace ImageCharts.Net
{
    /// <summary>
    /// Describes the properties of the shape markers that can be used to highlight the data points of a series
    /// </summary>
    public class ShapeMarker
    {
        /// <summary>
        /// The visual style (shape) of the shape markers
        /// </summary>
        public ShapeMarkerType ShapeMarkerType { get; set; }

        /// <summary>
        /// The color in which the shape markers will be displayed
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// The size of the shape markers in pixels
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Creates a new instance of a <see cref="ShapeMarker"/>
        /// </summary>
        /// <param name="shapeMarkerType">The visual style (shape) of the shape markers</param>
        /// <param name="color">The color in which the shape markers will be displayed</param>
        /// <param name="size">The size of the shape markers in pixels</param>
        public ShapeMarker(ShapeMarkerType shapeMarkerType, Color color, int size)
        {
            this.ShapeMarkerType = shapeMarkerType;
            this.Color = color;
            this.Size = size;
        }
    }
}
