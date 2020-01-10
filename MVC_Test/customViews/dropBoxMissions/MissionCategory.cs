using System.Collections.Generic;

namespace MVC_Test.customViews.dropBoxMissions {
	internal class MissionCategory : MissionDropBoxItem {
		public string Name;
		public List<MissionCategoryItem> ItemList;
		private MissionCategoryItem _selectedItem;

		public MissionCategoryItem SelectedItem {
			get => _selectedItem;
			set {
				_selectedItem = value;
				Title = $"{Name}: [{SelectedItem.Name}]";
			}
		}

		public MissionCategory(string name, List<MissionCategoryItem> itemList) : base(ItemType.Category,
																																						name) {
			Name = name;
			ItemList = itemList;
			if (itemList.Count > 0) { SelectedItem = itemList[0]; }
		}
	}
}