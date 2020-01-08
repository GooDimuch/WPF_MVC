using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MVC_Test.customViews.dropBoxWithCategories {
	public class HntVisibleConverter : IMultiValueConverter {
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
			var boolValue = false;
			var stringValue = string.Empty;

			foreach (var value in values) {
				switch (value) {
					case bool b:
						boolValue = b;
						break;
					default:
						stringValue = value.ToString();
						break;
				}
			}
			return Visibility.Visible;
			//			return !boolValue && string.IsNullOrEmpty(stringValue) ? Visibility.Visible : Visibility.Collapsed;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
			return new object[] { };
		}
	}

	public class BackgroundItemConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

			return ((DateTime) value).ToString("dd.MM.yyyy");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return DependencyProperty.UnsetValue;
		}
	}
}