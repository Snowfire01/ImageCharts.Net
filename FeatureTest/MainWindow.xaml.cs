using ImageCharts.Net.ChartProperties;
using ImageCharts.Net.Charts;
using ImageCharts.Net.Data;
using ImageCharts.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using DataFormat = ImageCharts.Net.Enums.DataFormat;
using Color = System.Drawing.Color;
using System.Linq;

namespace FeatureTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string baseUrl = "http://ws.audioscrobbler.com/2.0/";
        private readonly string apiKey = "6f8b4637265f2567eba6878b0e67c933";
        private readonly string secret = "7ab6921386c1e5d9f5fa9e679d68f4a4";

        public MainWindow()
        {
            InitializeComponent();

            this.ComboBoxEncoding.SelectedIndex = 0;
            this.ComboBoxCharType.SelectedIndex = 0;
            this.ComboBoxBackgroundFillType.SelectedIndex = 0;
            this.ComboBoxChartStyle.SelectedIndex = 0;

            this.TextBoxDataPoints.Text = "(fill:)-(lineStyle:)-(lineFill:)-(shapeMarker:)-(1,2^0)-(2,2^1)-(4,2^2)-(8,2^3)-(16,2^4)-(32,2^5)-(64,)" +
                "|(fill:gradient,90,009900,0;990099,1)-(lineStyle:)-(lineFill:)-(shapeMarker:)-(1,)-(10,10*1)-(20,10*2)-(30,10*3)-(40,10*4)-(50,10*5)-(60,10*6)";
            this.TextBoxChartWidth.Text = "500";
            this.TextBoxChartHeight.Text = "500";

            this.TextBoxChartTitle.Text = "TestChart";
            this.TextBoxLegendItems.Text = "serie1,serie2";

            this.TextBoxBackgroundFillGradientColors.Visibility = Visibility.Collapsed;
            this.TextBoxBackgroundFillGradientAngle.Visibility = Visibility.Collapsed;
            this.TextBoxBackgroundFillSingleColor.Visibility = Visibility.Visible;
        }

        private IEnumerable<DataSeries> GetDataSeries(string dataPointString)
        {
            var splittedSeries = dataPointString.Split('|');

            foreach (var series in splittedSeries)
            {
                var stuff = series.Split('-');
                var fill = stuff.First().Replace("(", "").Replace(")", "").Replace("fill:", "");
                var lineStyle = stuff.Skip(1).First().Replace("(", "").Replace(")", "").Replace("lineStyle:", "");
                var lineFill = stuff.Skip(2).First().Replace("(", "").Replace(")", "").Replace("lineFill:", "");
                var shapeMarker = stuff.Skip(3).First().Replace("(", "").Replace(")", "").Replace("shapeMarker:", "");
                var points = stuff.Skip(4);

                var dps = new List<DataPoint>();

                foreach (var point in points)
                {
                    var valueString = point.Replace("(", "").Replace(")", "");
                    var values = valueString.Split(',');

                    double? value = null;

                    if (double.TryParse(values[0], out var valueN))
                        value = valueN;

                    dps.Add(new DataPoint(value, values[1]));
                }

                var newSeries = new DataSeries(dps);

                if (fill != "")
                {
                    Fill realFill = null;

                    if (fill.StartsWith("single"))
                    {
                        realFill = new SingleColorFill { FillType = FillType.BackgroundFill, Color = this.GetColor(fill.Replace("single,", "")) };
                    }
                    else if (fill.StartsWith("multi"))
                    {
                        realFill = new MultiColorFill { FillType = FillType.BackgroundFill };

                        var singleColorFills = new List<SingleColorFill>();

                        foreach (var color in fill.Replace("multi,", "").Split(';'))
                        {
                            singleColorFills.Add(new SingleColorFill { FillType = FillType.BackgroundFill, Color = this.GetColor(color) });
                        }

                        ((MultiColorFill)realFill).Colors = singleColorFills;
                    }
                    else if (fill.StartsWith("gradient"))
                    {
                        var angle = 90;
                        int.TryParse(fill.Replace("gradient,", "").Substring(0, 2), out angle);

                        realFill = new GradientFill
                        {
                            Angle = angle,
                            FillType = FillType.BackgroundFill,
                            GradientColors = this.GetGradientColors(fill.Substring("gradient,".Length + 3))
                        };
                    }

                    newSeries.Fill = realFill;
                }

                if (lineStyle != "")
                {
                    var props = lineStyle.Split(',');

                    if (props.Length == 1)
                        newSeries.LineStyle = new LineStyle(Convert.ToInt32(props[0]));
                    else if (props.Length == 2)
                        newSeries.LineStyle = new LineStyle(Convert.ToInt32(props[0]), Convert.ToInt32(props[1]));
                    else if (props.Length == 3)
                        newSeries.LineStyle = new LineStyle(Convert.ToInt32(props[0]), Convert.ToInt32(props[1]), Convert.ToInt32(props[2]));
                }

                if (lineFill != "")
                {
                    var props = lineFill.Split(',');

                    if (props[0] == "under")
                    {
                        newSeries.LineFill = new LineFill(LineFillType.UnderLine, this.GetColor(props[1]));
                    }
                    else if (props[0] == "between")
                    {
                        newSeries.LineFill = new LineFill(LineFillType.BetweenLines, this.GetColor(props[1]), Convert.ToInt32(props[2]));
                    }
                }

                if (shapeMarker != "")
                {
                    var props = shapeMarker.Split(',');

                    var type = (ShapeMarkerType)Enum.Parse(typeof(ShapeMarkerType), props[0]);
                    var color = this.GetColor(props[1]);
                    var size = Convert.ToInt32(props[2]);

                    newSeries.ShapeMarker = new ShapeMarker(type, color, size);
                }

                yield return newSeries;
            }
        }

        private IEnumerable<GradientColor> GetGradientColors(string colorsString)
        {
            var splitted = colorsString.Split(';');

            foreach (var item in splitted)
            {
                var thingy = item.Replace("(", "").Replace(")", "").Split(',');

                var color = this.GetColor(thingy[0]);
                var centerPoint = Convert.ToDouble(thingy[1]);

                yield return new GradientColor(color, centerPoint);
            }
        }

        private void ComboBoxCharType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var chartType = ((ComboBoxItem)this.ComboBoxCharType.SelectedItem).Content.ToString().ToLower();

            if (chartType == "barchart")
            {
                this.ComboBoxChartStyle.ItemsSource = new List<string>
                {
                    "GroupedVertically",
                    "GroupedHorizontally",
                    "StackedVertically",
                    "StackedHorizontally"
                };

                this.ComboBoxChartStyle.SelectedIndex = 0;
            }
            else if (chartType == "linechart")
            {
                this.ComboBoxChartStyle.ItemsSource = new List<string>
                {
                    "Regular",
                    "NoAxisLines",
                    "SpecifyCoordinates"
                };

                this.ComboBoxChartStyle.SelectedIndex = 0;
            }
            else if (chartType == "piechart")
            {
                this.ComboBoxChartStyle.ItemsSource = new List<string>
                {
                    "Regular2D",
                    "Regular3D",
                    "Concentric",
                    "Doughnut"
                };



                this.ComboBoxChartStyle.SelectedIndex = 0;
            }
        }

        private void ComboBoxBackgroundFillType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBoxItem)this.ComboBoxBackgroundFillType.SelectedItem).Content.ToString().ToLower() == "singlecolor")
            {
                this.TextBoxBackgroundFillGradientColors.Visibility = Visibility.Collapsed;
                this.TextBoxBackgroundFillGradientAngle.Visibility = Visibility.Collapsed;
                this.TextBlockBackgroundFillGradientColors.Visibility = Visibility.Collapsed;
                this.TextBlockBackgroundFillGradientAngle.Visibility = Visibility.Collapsed;

                this.TextBoxBackgroundFillSingleColor.Visibility = Visibility.Visible;
                this.TextBlockBackgroundFillSingleColor.Visibility = Visibility.Visible;
            }
            else if (((ComboBoxItem)this.ComboBoxBackgroundFillType.SelectedItem).Content.ToString().ToLower() == "gradient")
            {
                this.TextBoxBackgroundFillGradientColors.Visibility = Visibility.Visible;
                this.TextBoxBackgroundFillGradientAngle.Visibility = Visibility.Visible;
                this.TextBlockBackgroundFillGradientColors.Visibility = Visibility.Visible;
                this.TextBlockBackgroundFillGradientAngle.Visibility = Visibility.Visible;

                this.TextBoxBackgroundFillSingleColor.Visibility = Visibility.Collapsed;
                this.TextBlockBackgroundFillSingleColor.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = new ChartData(this.GetDataSeries(TextBoxDataPoints.Text))
                {
                    DataFormat = (DataFormat)Enum.Parse(typeof(DataFormat), ((ComboBoxItem)this.ComboBoxEncoding.SelectedItem).Content.ToString())
                };

                var height = Convert.ToInt32(this.TextBoxChartHeight.Text);
                var width = Convert.ToInt32(this.TextBoxChartWidth.Text);

                var chartTitle = new ChartTitle(this.TextBoxChartTitle.Text);

                if (this.TextBoxChartTitleFont.Text != "") chartTitle.FontSize = Convert.ToInt32(this.TextBoxChartTitleFont.Text);
                if (this.TextBoxChartTitleColor.Text != "") chartTitle.TextColor = this.GetColor(this.TextBoxChartTitleColor.Text);

                //if (this.)

                var margin = (0, 0, 0, 0);

                if (!((this.TextBoxMarginLeft.Text + this.TextBoxMarginRight.Text + this.TextBoxMarginTop.Text + this.TextBoxMarginBottom.Text) == ""))
                {
                    int left, right, top, bottom;

                    int.TryParse(TextBoxMarginLeft.Text, out left);
                    int.TryParse(TextBoxMarginRight.Text, out right);
                    int.TryParse(TextBoxMarginTop.Text, out top);
                    int.TryParse(TextBoxMarginBottom.Text, out bottom);

                    margin = (left, right, top, bottom);
                }

                var legendItems = this.TextBoxLegendItems.Text.Split(',');

                var backgroundFill = ((ComboBoxItem)this.ComboBoxBackgroundFillType.SelectedItem).Content.ToString().ToLower();
                Fill chartFill = null;

                if (backgroundFill == "singlecolor" && this.TextBoxBackgroundFillSingleColor.Text.Length >= 6)
                {
                    chartFill = new SingleColorFill { FillType = FillType.ChartAreaFill, Color = this.GetColor(this.TextBoxBackgroundFillSingleColor.Text) };
                }
                else if (backgroundFill == "gradient" && this.TextBoxBackgroundFillGradientColors.Text != "")
                {
                    var angle = 90;
                    int.TryParse(this.TextBoxBackgroundFillGradientAngle.Text, out angle);

                    chartFill = new GradientFill
                    {
                        Angle = angle,
                        FillType = FillType.BackgroundFill,
                        GradientColors = this.GetGradientColors(this.TextBoxBackgroundFillGradientColors.Text)
                    };
                }

                var chartType = ((ComboBoxItem)this.ComboBoxCharType.SelectedItem).Content.ToString().ToLower();
                var chartStyle = this.ComboBoxChartStyle.SelectedItem.ToString().ToLower();

                Chart chart = null;

                if (chartType == "barchart")
                {
                    chart = new BarChart(data, (BarChartStyle)Enum.Parse(typeof(BarChartStyle), chartStyle, true))
                    {
                        ChartTitle = chartTitle,
                        LegendItems = legendItems,
                        ChartHeight = height,
                        ChartWidth = width,
                        Margin = margin,
                        Fill = chartFill
                    };
                }
                else if (chartType == "piechart")
                {
                    var pieChart = new PieChart(data, (PieChartStyle)Enum.Parse(typeof(PieChartStyle), chartStyle, true))
                    {
                        ChartTitle = chartTitle,
                        LegendItems = legendItems,
                        ChartHeight = height,
                        ChartWidth = width,
                        Margin = margin,
                        Fill = chartFill
                    };

                    chart = pieChart;
                }
                else if (chartType == "linechart")
                {
                    chart = new LineChart(data, (LineChartStyle)Enum.Parse(typeof(LineChartStyle), chartStyle, true))
                    {
                        ChartTitle = chartTitle,
                        LegendItems = legendItems,
                        ChartHeight = height,
                        ChartWidth = width,
                        Margin = margin,
                        Fill = chartFill
                    };
                }
                else if (chartType == "polarchart")
                {
                    chart = new PolarChart(data)
                    {
                        ChartTitle = chartTitle,
                        LegendItems = legendItems,
                        ChartHeight = height,
                        ChartWidth = width,
                        Margin = margin,
                        Fill = chartFill
                    };
                }

                var url = chart?.GetUrl() ?? "https://s14-eu5.startpage.com/cgi-bin/serveimage?url=http%3A%2F%2Ft0.gstatic.com%2Fimages%3Fq%3Dtbn%3AANd9GcQCD2erBBwErx8NMhBWS7WC2A0Ffq-2F7g0twTJZUhsWUKUANyeSg&sp=9b17fb40fb9f165b4039ad7762b9ae08&anticache=578534";
                this.TextBoxUrl.Text = url;
                this.ChartImage.Source = new BitmapImage(new Uri(url));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        private Color GetColor(string colorString)
        {
            var r = Convert.ToByte(colorString.Substring(0, 2));
            var g = Convert.ToByte(colorString.Substring(2, 2));
            var b = Convert.ToByte(colorString.Substring(4, 2));

            if (colorString.Length > 6)
                return Color.FromArgb(Convert.ToByte(colorString.Substring(6, 2)), r, g, b);
            else
                return Color.FromArgb(r, g, b);
        }
    }

    class Scrobble
    {
        [JsonProperty("artist")]
        public Artist Artist { get; set; }

        [JsonProperty("@attr")]
        public Attributes Attribute { get; set; }

        [JsonProperty("mbid")]
        public string MbId { get; set; }

        [JsonProperty("streamable")]
        public string Streamable { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("date")]
        public Date Date { get; set; }

        public override string ToString() => this.Name;
    }

    class Date
    {
        public long UnixTimeStamp { get; set; }

        public DateTime DateTime { get; set; }

        public override string ToString() => $"{this.DateTime:dd.MM.yyy HH:mm}";
    }

    class Album
    {
        [JsonProperty("mbid")]
        public string MbId { get; set; }

        [JsonProperty("#text")]
        public string Name { get; set; }

        public override string ToString() => this.Name;
    }

    class Artist
    {
        [JsonProperty("Mbid")]
        string MbId { get; set; }

        [JsonProperty("#text")]
        public string Name { get; set; }

        public override string ToString() => this.Name;
    }

    class Attributes
    {
        [JsonProperty("nowplaying")]
        public bool NowPlaying { get; set; }
    }
}
