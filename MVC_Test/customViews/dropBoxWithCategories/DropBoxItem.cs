using System.Windows.Media;

namespace MVC_Test.customViews.dropBoxWithCategories {
	internal class DropBoxItem {
		public ItemType Type { get; set; }
		public string Title { get; set; }
		public SolidColorBrush BackgroundBrush { get; set; }

		public DropBoxItem(ItemType type, string title) {
			Type = type;
			Title = title;
			BackgroundBrush = Type == ItemType.Category ? Brushes.DarkGray : Brushes.LightGray;
		}

		public override string ToString() { return Title; }
	}
}