namespace MVC_Test.customViews.dropBoxMissions {
	internal class Mission : MissionDropBoxItem {
		public string Name;
		public MissionDropBoxModel Model;

		public Mission(string name, MissionDropBoxModel model) : base(ItemType.Mission, name) {
			Name = name;
			Model = model;
		}
	}
}