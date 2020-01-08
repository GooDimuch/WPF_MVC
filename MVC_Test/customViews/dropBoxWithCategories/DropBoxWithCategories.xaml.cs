using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MVC_Test.customViews.dropBoxWithCategories {
	/// <summary>
	/// Логика взаимодействия для DropBoxWithCategories.xaml
	/// </summary>
	public partial class DropBoxWithCategories : UserControl {
		public const string xml = "<dropbox_models>" +
															"		<dropbox_model name = \"model1\" title=\"\" >" +
															"" +
															"			<category title = \"Режим\" >" +
															"				<category_item content=\"реж_1\" key=\"-0\" />" +
															"				<category_item content=\"реж_2\" key=\"-1\" />" +
															"				<category_item content=\"реж_3\" key=\"-2\" />" +
															"			</category>" +
															"" +
															"			<category title = \"Погода\" >" +
															"				<category_item content=\"ясно\" key=\"-0\" />" +
															"				<category_item content=\"дождь\" key=\"-1\" />" +
															"				<category_item content=\"снег\" key=\"-2\" />" +
															"			</category>" +
															"" +
															"		</dropbox_model>" +
															"</dropbox_models>";

		public enum ItemType {
			Category,
			Item
		}

		private BaseDropBoxModel Model;
		private BaseCategory SelectedCategory;
		private bool isChanged;

		public DropBoxWithCategories() {
			InitializeComponent();
			Model = CreateDropBoxModel();
			DropBox.ItemsSource = new List<DropBoxItem>(Model.CategoryList);
		}

		private void DropBoxWithCategories_OnLoaded(object sender, RoutedEventArgs e) {
			DropBox.DropDownOpened += DropDownOpened;
			DropBox.DropDownClosed += DropDownClosed;
		}

		private static BaseDropBoxModel CreateDropBoxModel() {
			var item11 = new BaseCategoryItem("реж_1", "0");
			var item12 = new BaseCategoryItem("реж_2", "1");
			var item13 = new BaseCategoryItem("реж_3", "2");
			var category1 = new BaseCategory("Режим", new List<BaseCategoryItem> {item11, item12, item13});
			var item21 = new BaseCategoryItem("ясно", "0");
			var item22 = new BaseCategoryItem("дождь", "1");
			var item23 = new BaseCategoryItem("снег", "2");
			var category2 = new BaseCategory("Погода", new List<BaseCategoryItem> {item21, item22, item23});
			return new BaseDropBoxModel("model1", new List<BaseCategory> {category1, category2});
		}

		private void EventButton_OnClick(object sender, RoutedEventArgs e) {
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
		}

		private void DropDownOpened(object sender, EventArgs e) { }

		private void DropDownClosed(object sender, EventArgs e) {
			if (isChanged) {
				isChanged = false;
				DropBox.IsDropDownOpen = true;
			} else { }
		}

		/// <summary>
		/// Изначально берем вписок всех категорий. Потом определяем тип нажатого объекта. Если это категория, то проверяем
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
			var itemList = Model.CategoryList.Cast<DropBoxItem>().ToList();
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
					category = Model.CategoryList.ToList().Find(baseCategory => baseCategory.ItemList.Contains(item));
					//todo указать в поле SelectedItem выбранный элемент
					category.SelectedItem = item as BaseCategoryItem ?? throw new Exception("can't cast item to BaseCategory");
					//todo очистить 
					SelectedCategory = null;
					break;
				default: throw new ArgumentOutOfRangeException();
			}
			//todo отобразить итоговый список элементов
			DropBox.ItemsSource = itemList;
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

	public class BaseDropBoxModel {
		public string Name;
		public List<BaseCategory> CategoryList;

		public BaseDropBoxModel(string name, List<BaseCategory> categoryList) {
			Name = name;
			CategoryList = categoryList;
		}
	}

	public class BaseCategory : DropBoxItem {
		public List<BaseCategoryItem> ItemList;
		public string Name;
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