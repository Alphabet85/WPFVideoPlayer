using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFVideoPlayer.Resources.Converters
{
    /// <summary>
    /// BooleanToVisibilityCollapsedConverter
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if (value != null)
                {
                    if ((bool)value)
                        return Visibility.Visible;
                    else
                        return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    /// <summary>
    /// BooleanToVisibilityHiddenConverter
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityHiddenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if (value != null)
                {
                    if ((bool)value)
                        return Visibility.Visible;
                    else
                        return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    /// <summary>
    /// BooleanNotToVisibilityCollapsedConverter
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanNotToVisibilityCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if (value != null)
                {
                    if ((bool)value)
                        return Visibility.Collapsed;
                    else
                        return Visibility.Visible;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    /// <summary>
    /// BooleanNotToVisibilityHiddenConverter
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanNotToVisibilityHiddenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if (value != null)
                {
                    if ((bool)value)
                        return Visibility.Hidden;
                    else
                        return Visibility.Visible;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
