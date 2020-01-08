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

		public DropBoxWithCategories() {
			InitializeComponent();
			Model = CreateDropBoxModel();
			DropBox.ItemsSource = new List<DropBoxItem>(Model.CategoryList);
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

		private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e) {
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
			if (!(sender is DropBoxItem item)) { return; }
			List<DropBoxItem> itemList = new List<DropBoxItem>();
			switch (item.Type) {
				case ItemType.Category:
					//todo взять список категорий
					itemList = Model.CategoryList.Cast<DropBoxItem>().ToList();
					//todo найти индекс выбранной категории

					//todo вставить по индексу все элементы выбранной категории
					break;
				case ItemType.Item:
					//todo найти категорию выброного элемента
					//todo указать в поле SelectedItem выбранный элемент
					//todo взять список категорий
					itemList = Model.CategoryList.Cast<DropBoxItem>().ToList();
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
		public string Title;
		public List<BaseCategoryItem> ItemList;
		public BaseCategoryItem SelectedItem;

		public BaseCategory(string title, List<BaseCategoryItem> itemList) : base(DropBoxWithCategories.ItemType.Category,
																																							title) {
			Title = title;
			ItemList = itemList;
			if (itemList.Count > 0) { SelectedItem = itemList[0]; }
		}
	}

	public class BaseCategoryItem : DropBoxItem {
		public string Title;
		public string Key;

		public BaseCategoryItem(string title, string key) : base(DropBoxWithCategories.ItemType.Item, title) {
			Title = title;
			Key = key;
		}
	}
}