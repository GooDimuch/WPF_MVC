using System;
using MVC_Test.windows.basePage;

namespace MVC_Test.windows.mainWindow.secondPage {
	internal class SecondPageController : BasePageController {
		private ISecondPage View => _view as ISecondPage;

		public SecondPageController(IBasePage view) : base(view) { Console.WriteLine("SecondPageController"); }

		public void SetBackground() { View.SecondPageMethod(); }
	}
}