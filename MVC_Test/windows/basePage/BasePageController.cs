using System;
using System.Collections.Generic;

namespace MVC_Test.windows.basePage {
	public abstract class BasePageController {
		public static List<BasePageController> ControllerList = new List<BasePageController>();
		protected readonly IBasePage _view;

		protected BasePageController(IBasePage view) {
			Console.WriteLine("BasePageController");
			_view = view;
			ControllerList.Add(this);
		}
	}
}