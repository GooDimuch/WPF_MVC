using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MVC_Test.windows.mainWindow.firstPage;

namespace MVC_Test.customViews.dropBoxWithCategories {
	/// <summary>
	/// Логика взаимодействия для DropBoxWithCategories.xaml
	/// </summary>
	public partial class DropBoxWithCategories : INotifyPropertyChanged {
		public enum ItemType {
			Category,
			Item
		}

		private BaseCategory SelectedCategory;
		private bool isChanged;

		private RoutedEventHandler _click;
		public event RoutedEventHandler Click { add => _click += value; remove => _click -= value; }

		public static DependencyProperty SourceProperty =
			DependencyProperty.Register(nameof(Source), typeof(BaseDropBoxModel), typeof(DropBoxWithCategories));

		public BaseDropBoxModel Source {
			get => (BaseDropBoxModel) GetValue(SourceProperty);
			set {
				SetValue(SourceProperty, value);
				DropBox.ItemsSource = Source.CategoryList;
				UpdateTitle();
			}
		}

		private string _title;

		public string Title {
			get => _title;
			set {
				_title = value;
				OnPropertyChanged(nameof(Title));
			}
		}

		public string KeyList {
			get {
				var sb = new StringBuilder();
				Source.CategoryList.ForEach(category => sb.Append($"-{category.Name}={category.SelectedItem.Key} "));
				return sb.ToString();
			}
		}

		public DropBoxWithCategories() {
			InitializeComponent();
			DataContext = this;
		}

		private void DropBoxWithCategories_OnLoaded(object sender, RoutedEventArgs e) {
			DropBox.DropDownOpened += DropDownOpened;
			DropBox.DropDownClosed += DropDownClosed;
		}

		private void EventButton_OnClick(object sender, RoutedEventArgs e) {
			_click?.Invoke(this, e);
			DropBox.IsDropDownOpen = false;
		}

		private void DropDownOpened(object sender, EventArgs e) { }

		private void DropDownClosed(object sender, EventArgs e) {
			if (isChanged) {
				isChanged = false;
				DropBox.IsDropDownOpen = true;
			} else { }
		}

		/// <summary>
		/// Изначально берем cписок всех категорий. Потом определяем тип нажатого объекта. Если это категория, то проверяем
		/// открыта ли она сейчас, если открыта, то закрываем, а если закрыта, то берем у нее список всех дочерних объектов
		/// и вставляем по (индексу категории + 1)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e) {
			if (isChanged) { return; } else { isChanged = true; }
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
			if (!(sender is ComboBoxItem cbItem) || !cbItem.Content.GetType().IsSubclassOf(typeof(DropBoxItem))) { return; }
			var item = cbItem.Content as DropBoxItem ?? throw new Exception("can't cast item to DropBoxItem");
			//todo взять список категорий
			var itemList = Source.CategoryList.Cast<DropBoxItem>().ToList();
			BaseCategory category;
			switch (item.Type) {
				case ItemType.Category:
					category = item as BaseCategory ?? throw new Exception("can't cast item to BaseCategory");
					if (SelectedCategory?.Equals(category) ?? false) {
						SelectedCategory = null;
						break;
					}
					//todo найти индекс выбранной категории
					var selectedIndex = itemList.IndexOf(category);
					//todo вставить по индексу все элементы выбранной категории
					itemList.InsertRange(selectedIndex + 1, category.ItemList);
					SelectedCategory = category;
					break;
				case ItemType.Item:
					//todo найти категорию выброного элемента
					category = Source.CategoryList.ToList().Find(baseCategory => baseCategory.ItemList.Contains(item));
					//todo указать в поле SelectedItem выбранный элемент
					category.SelectedItem = item as BaseCategoryItem ?? throw new Exception("can't cast item to BaseCategory");
					//todo очистить 
					SelectedCategory = null;
					//todo update title
					UpdateTitle();
					break;
				default: throw new ArgumentOutOfRangeException();
			}
			//todo отобразить итоговый список элементов
			DropBox.ItemsSource = itemList;
		}

		private void UpdateTitle() {
			var sb = new StringBuilder();
			Source?.CategoryList.ForEach(category => sb.Append($"{category.SelectedItem.Name}, "));
			if (sb.Length > 2) sb.Remove(sb.Length - 2, 2);
			Title = sb.ToString();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	public class DropBoxItem {
		public DropBoxWithCategories.ItemType Type { get; set; }
		public string Title { get; set; }
		public SolidColorBrush BackgroundBrush { get; set; }

		public DropBoxItem(DropBoxWithCategories.ItemType type, string title) {
			Type = type;
			Title = title;
			BackgroundBrush = Type == DropBoxWithCategories.ItemType.Category ? Brushes.DarkGray : Brushes.LightGray;
		}

		public override string ToString() { return Title; }
	}

	public class BaseCategory : DropBoxItem {
		public string Name;
		public List<BaseCategoryItem> ItemList;
		private BaseCategoryItem _selectedItem;

		public BaseCategoryItem SelectedItem {
			get => _selectedItem;
			set {
				_selectedItem = value;
				Title = $"{Name}: [{SelectedItem.Name}]";
			}
		}

		public BaseCategory(string name, List<BaseCategoryItem> itemList) : base(DropBoxWithCategories.ItemType.Category,
																																						name) {
			Name = name;
			ItemList = itemList;
			if (itemList.Count > 0) { SelectedItem = itemList[0]; }
		}
	}

	public class BaseCategoryItem : DropBoxItem {
		public string Key;
		public string Name;

		public BaseCategoryItem(string name, string key) : base(DropBoxWithCategories.ItemType.Item, name) {
			Name = name;
			Key = key;
		}
	}
}