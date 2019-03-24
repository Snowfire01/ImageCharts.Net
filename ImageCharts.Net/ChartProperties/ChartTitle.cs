using System.Drawing;

namespace ImageCharts.Net.ChartProperties
{
    /// <summary>
    /// Describes the properties of the title text of a chart
    /// </summary>
    public class ChartTitle
    {
        /// <summary>
        /// The text that will be displayed as the title of the chart. Warning: line breaks don't work!
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// The font color of the title. Default is black.
        /// </summary>
        public Color TextColor { get; set; } = Color.Black;

        /// <summary>
        /// The font size of the title
        /// </summary>
        public int FontSize { get; set; } = 15;


        /// <summary>
        /// Initializes a new instance of <see cref="ChartTitle"/>
        /// </summary>
        /// <param name="text">The text that will be displayed as the title of the chart. Warning: line breaks don't work!</param>
        public ChartTitle(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ChartTitle"/>
        /// </summary>
        /// <param name="text">The text that will be displayed as the title of the chart. Warning: line breaks don't work!</param>
        /// <param name="textColor">The font color of the title</param>
        /// <param name="fontSize">The font size of the title</param>
        public ChartTitle(string text, Color textColor, int fontSize = 15)
        {
            this.Text = text;
            this.TextColor = textColor;
            this.FontSize = fontSize;
        }

        /// <summary>
        /// Returns a string with all spaces replaced by '+', as specified by imagecharts
        /// </summary>
        public override string ToString() => this.Text.Replace(' ', '+');
    }
}
