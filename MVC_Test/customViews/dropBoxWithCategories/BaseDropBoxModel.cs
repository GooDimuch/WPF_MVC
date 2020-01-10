using System.Collections.Generic;

namespace MVC_Test.customViews.dropBoxWithCategories {
	internal class BaseDropBoxModel {
		public string Name;
		public List<BaseCategory> CategoryList;

		public BaseDropBoxModel(string name, List<BaseCategory> categoryList) {
			Name = name;
			CategoryList = categoryList;
		}
	}
}