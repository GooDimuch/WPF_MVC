namespace MVC_Test.customViews.dropBoxMissions {
	internal class MissionCategoryItem : MissionDropBoxItem {
		public string Key;
		public string Name;

		public MissionCategoryItem(string name, string key) : base(ItemType.Item, name) {
			Name = name;
			Key = key;
		}
	}
}