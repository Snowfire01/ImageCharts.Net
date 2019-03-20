using ImageCharts.Net.ChartProperties;
using ImageCharts.Net.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ImageCharts.Net.Helpers
{
    internal static class ChartDataEncoder
    {
        private static string encoderString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-.";

        internal static string GetScalingSpecifier(ChartData data)
        {
            switch (data.DataFormat)
            {

                case DataFormat.TextFormatAutomaticScaling:
                    return "a";
                case DataFormat.TextFormatCustomScaling:
                    var stringifiedCustomScalings = data.CustomScalings.Select(x => $"{x.Min},{x.Max}");
                    return string.Join(",", stringifiedCustomScalings);
                default:
                    return string.Empty;
            }
        }

        internal static string GetFormatSpecifier(ChartData data)
        {
            switch (data.DataFormat)
            {
                case DataFormat.AwesomeDataFormat:
                    return "a";
                case DataFormat.BasicTextFormat:
                case DataFormat.TextFormatAutomaticScaling:
                case DataFormat.TextFormatCustomScaling:
                    return "t";
                case DataFormat.SimpleEncodingFormat:
                    return "s";
                case DataFormat.ExtendedEncodingFormat:
                    return "e";
                default:
                    throw new ArgumentException("Not a valid data format", $"{nameof(data)}.{nameof(data.DataFormat)}");
            }
        }

        internal static string GetEncodedValues(ChartData data)
        {
            switch (data.DataFormat)
            {
                case DataFormat.AwesomeDataFormat:
                    return AwesomeDataFormat(data);
                case DataFormat.BasicTextFormat:
                case DataFormat.TextFormatAutomaticScaling:
                case DataFormat.TextFormatCustomScaling:
                    return BasicTextFormat(data);
                case DataFormat.SimpleEncodingFormat:
                    return SimpleEncodingFormat(data);
                case DataFormat.ExtendedEncodingFormat:
                    return ExtendedEncodingFormat(data);
                default:
                    throw new ArgumentException("Not a valid data format", $"{nameof(data)}.{nameof(data.DataFormat)}");
            }
        }

        private static string AwesomeDataFormat(ChartData data)
        {
            var stringBuilder = new StringBuilder();

            foreach (var dataSeries in data.DataSeries)
            {
                var fittedValues = dataSeries.DataPoints.Select(x => x.Value).ToList();

                var stringifiedValues = fittedValues.Select(x => x?.ToString(CultureInfo.GetCultureInfo("en-us")) ?? "_");

                stringBuilder.Append(string.Join(",", stringifiedValues));
                stringBuilder.Append("|");
            }

            var returnValue = stringBuilder.ToString().Trim('|');

            return returnValue;
        }

        private static string BasicTextFormat(ChartData data)
        {
            var stringBuilder = new StringBuilder();

            foreach (var dataSeries in data.DataSeries)
            {
                var fittedValues = dataSeries.DataPoints.Select(x => x.Value).ToList();

                var stringifiedValues = fittedValues.Select(x => x?.ToString(CultureInfo.GetCultureInfo("en-us")) ?? "_");

                stringBuilder.Append(string.Join(",", stringifiedValues));
                stringBuilder.Append("|");
            }

            var returnValue = stringBuilder.ToString().Trim('|');

            return returnValue;
        }

        private static string SimpleEncodingFormat(ChartData data)
        {
            var stringBuilder = new StringBuilder();

            foreach (var dataSeries in data.DataSeries)
            {
                var fittedValues = dataSeries.DataPoints.Select(x =>
                {
                    if (x.Value < 0)
                        return null;
                    else if (x.Value > 61)
                        return 61;
                    else
                        return (int?) x.Value;
                }).ToList();

                foreach (var value in fittedValues)
                {
                    if (value.HasValue)
                    {
                        stringBuilder.Append(encoderString[value.Value]);
                    }
                    else
                    {
                        stringBuilder.Append("_");
                    }
                }

                stringBuilder.Append(",");
            }

            var returnValue = stringBuilder.ToString().Trim(',');

            return returnValue;
        }

        private static string ExtendedEncodingFormat(ChartData data)
        {
            var stringBuilder = new StringBuilder();

            foreach (var dataSeries in data.DataSeries)
            {
                var fittedValues = dataSeries.DataPoints.Select(x =>
                {
                    if (x.Value < 0)
                        return null;
                    else if (x.Value > 4095)
                        return 4095;
                    else
                        return (int?)x.Value;
                }).ToList();

                foreach (var value in fittedValues)
                {
                    if (value.HasValue)
                    {
                        var valueCache = value.Value;

                        var digits = new List<int>();
                        
                        while (valueCache > 0)
                        {
                            digits.Insert(0, valueCache % 64);
                            valueCache = valueCache / 64;
                        }

                        while (digits.Count < 2)
                            digits.Insert(0, 0);

                        foreach (var i in digits)
                        {
                            stringBuilder.Append(encoderString[i]);
                        }
                    }
                    else
                    {
                        stringBuilder.Append("__");
                    }
                }

                stringBuilder.Append(",");
            }

            var returnValue = stringBuilder.ToString().Trim(',');

            return returnValue;
        }
    }
}
