using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SfDataGrid_Demo
{
    /// <summary>
    /// A generic converter that can be parameterized for different columns
    /// Simulates conversion logic per column
    /// </summary>
    public class GenericValueConverter : IValueConverter
    {
        private readonly int _colIndex;

        public GenericValueConverter(int colIndex)
        {
            _colIndex = colIndex;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "[NULL]";

            // Simulate converter logic: prefix with column index
            return $"[C{_colIndex}] {value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    /// <summary>
    /// Converter for row background styling based on data conditions.
    /// Returns different colors based on the row index or data values.
    /// </summary>
    public class RowBackgroundStyleConverter : IValueConverter
    {
        private static readonly SolidColorBrush PositiveRowBrush = new SolidColorBrush(Color.FromArgb(50, 0, 200, 0));    // Light green
        private static readonly SolidColorBrush NegativeRowBrush = new SolidColorBrush(Color.FromArgb(50, 200, 0, 0));    // Light red
        private static readonly SolidColorBrush NeutralRowBrush = new SolidColorBrush(Color.FromArgb(50, 200, 200, 0));   // Light yellow
        private static readonly SolidColorBrush DefaultRowBrush = new SolidColorBrush(Colors.Transparent);                // Transparent

        /// <summary>
        /// Converts a value to a row background brush based on styling rules.
        /// Parameter can specify the style type: "RowIndex", "Numeric", or "Alternating"
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DefaultRowBrush;

            string styleType = parameter as string ?? "RowIndex";

            switch (styleType.ToLower())
            {
                case "rowindex":
                    // Style based on row index (alternating pattern)
                    if (value is int rowIndex)
                    {
                        return (rowIndex % 2 == 0) ? PositiveRowBrush : DefaultRowBrush;
                    }
                    break;

                case "numeric":
                    // Style based on numeric value (positive/negative/zero)
                    if (value is double numValue)
                    {
                        if (numValue > 0) return PositiveRowBrush;
                        if (numValue < 0) return NegativeRowBrush;
                        return NeutralRowBrush;
                    }
                    if (value is int intValue)
                    {
                        if (intValue > 0) return PositiveRowBrush;
                        if (intValue < 0) return NegativeRowBrush;
                        return NeutralRowBrush;
                    }
                    break;

                case "alternating":
                    // Simple alternating colors (3-color pattern)
                    if (value is int altIndex)
                    {
                        return (altIndex % 3 == 0) ? PositiveRowBrush : 
                               (altIndex % 3 == 1) ? NegativeRowBrush : 
                               NeutralRowBrush;
                    }
                    break;

                case "boolean":
                    // Style based on boolean value
                    if (value is bool boolValue)
                    {
                        return boolValue ? PositiveRowBrush : NegativeRowBrush;
                    }
                    break;
            }

            return DefaultRowBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converter for alternating row styling similar to WMMStrategyDataGrid
    /// </summary>
    public class AlternateRowStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int index)
            {
                return (index % 2 == 0) ? System.Windows.FontWeights.Normal : System.Windows.FontWeights.Bold;
            }
            return System.Windows.FontWeights.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converter for value change foreground color
    /// </summary>
    public class ValueChangeForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int index && index > 0 && index % 100 == 0)
            {
                return new SolidColorBrush(Colors.DarkRed);
            }
            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
