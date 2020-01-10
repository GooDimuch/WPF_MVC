using System.Collections.Generic;

namespace MVC_Test.customViews.dropBoxWithCategories {
	internal class BaseCategory : DropBoxItem {
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

		public BaseCategory(string name, List<BaseCategoryItem> itemList) : base(ItemType.Category, name) {
			Name = name;
			ItemList = itemList;
			if (itemList.Count > 0) { SelectedItem = itemList[0]; }
		}
	}
}