namespace MVC_Test.customViews.dropBoxWithCategories {
	internal class BaseCategoryItem : DropBoxItem {
		public string Key;
		public string Name;

		public BaseCategoryItem(string name, string key) : base(ItemType.Item, name) {
			Name = name;
			Key = key;
		}
	}
}