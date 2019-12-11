using System;
using MVC_Test.windows.basePage;

namespace MVC_Test.windows.mainWindow.firstPage {
	internal class FirstPageController : BasePageController {
		private IFirstPage View => _view as IFirstPage;
		public FirstPageController(IBasePage view) : base(view) { Console.WriteLine("FirstPageController"); }

		public void SetBackground() { View.FirstPageMethod(); }
	}
}