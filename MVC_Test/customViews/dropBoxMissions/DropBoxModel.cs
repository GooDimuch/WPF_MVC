using System.Collections.Generic;

namespace MVC_Test.customViews.dropBoxMissions {
	internal class DropBoxModel {
		public List<Mission> MissionList;

		public DropBoxModel(List<Mission> missionList) { MissionList = missionList; }
	}
}