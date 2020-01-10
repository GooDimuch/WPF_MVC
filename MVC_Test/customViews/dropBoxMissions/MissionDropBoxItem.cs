using System.Windows.Media;

namespace MVC_Test.customViews.dropBoxMissions {
	internal class MissionDropBoxItem {
		public ItemType Type { get; set; }
		public string Title { get; set; }
		public SolidColorBrush BackgroundBrush { get; set; }

		public MissionDropBoxItem(ItemType type, string title) {
			Type = type;
			Title = title;
			BackgroundBrush = Type == ItemType.Category ? Brushes.DarkGray : Brushes.LightGray;
		}

		public override string ToString() { return Title; }
	}
}