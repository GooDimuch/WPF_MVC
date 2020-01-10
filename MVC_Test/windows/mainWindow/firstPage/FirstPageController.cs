using System;
using System.Collections.Generic;
using MVC_Test.customViews.dropBoxMissions;
using MVC_Test.customViews.dropBoxWithCategories;
using MVC_Test.windows.basePage;

namespace MVC_Test.windows.mainWindow.firstPage {
	internal class FirstPageController : BasePageController {
		private IFirstPage View => _view as IFirstPage;
		public FirstPageController(IBasePage view) : base(view) { Console.WriteLine("FirstPageController"); }

		public void SetBackground() { View.FirstPageMethod(); }

		public BaseDropBoxModel GetDropBoxModel() {
			const string xml = "<dropbox_models>" +
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
			return CreateDropBoxModel(xml);
		}

		private static BaseDropBoxModel CreateDropBoxModel(string xml) {
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

		public DropBoxModel GetDropBoxMissionModel() {
			var list = new List<Mission> {
																		new Mission("mission_1", CreateDropBoxMissionModel("")),
																		new Mission("mission_2", CreateDropBoxMissionModel("")),
																		new Mission("mission_3", CreateDropBoxMissionModel(""))
																	};
			return new DropBoxModel(list);
		}

		private static MissionDropBoxModel CreateDropBoxMissionModel(string xml) {
			var item11 = new MissionCategoryItem("реж_1", "0");
			var item12 = new MissionCategoryItem("реж_2", "1");
			var item13 = new MissionCategoryItem("реж_3", "2");
			var category1 = new MissionCategory("Режим", new List<MissionCategoryItem> {item11, item12, item13});
			var item21 = new MissionCategoryItem("ясно", "0");
			var item22 = new MissionCategoryItem("дождь", "1");
			var item23 = new MissionCategoryItem("снег", "2");
			var category2 = new MissionCategory("Погода", new List<MissionCategoryItem> {item21, item22, item23});
			return new MissionDropBoxModel("model1", new List<MissionCategory> {category1, category2});
		}
	}
}