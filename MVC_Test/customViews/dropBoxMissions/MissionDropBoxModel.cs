using System.Collections.Generic;

namespace MVC_Test.customViews.dropBoxMissions {
	internal class MissionDropBoxModel {
		public string Name;
		public List<MissionCategory> CategoryList;

		public MissionDropBoxModel(string name, List<MissionCategory> categoryList) {
			Name = name;
			CategoryList = categoryList;
		}
	}
}