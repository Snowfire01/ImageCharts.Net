using System.Drawing;

namespace ImageCharts.Net.ChartProperties
{
    /// <summary>
    /// Encapsulates configuration options for the title text of a chart
    /// </summary>
    public class ChartTitle
    {
        /// <summary>
        /// The text that will be displayed as the title of the chart. Warning: line breaks don't work!
        /// </summary>
        public string Text { get; set; } = string.Empty;

        public Color TextColor { get; set; } = Color.FromArgb(alpha: 0xFF, red: 0x0, green: 0x0, blue: 0x0);

        public int FontSize { get; set; } = 15;

        public string GetTextColorString() => $"{this.TextColor.R:X2}{this.TextColor.G:X2}{this.TextColor.B:X2}{this.TextColor.A:X2}";

        public ChartTitle(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// Returns a string with all spaces replaced by '+', as specified by imagecharts
        /// </summary>
        public override string ToString() => this.Text.Replace(' ', '+');
    }
}
