using System;
using MVC_Test.windows.baseWindow;
using MVC_Test.windows.secondaryWindow.fourthPath;
using MVC_Test.windows.secondaryWindow.thirdPage;

namespace MVC_Test.windows.secondaryWindow {
	internal class SecondaryWindowController : BaseWindowController {
		private ISecondaryWindow View => _view as ISecondaryWindow;

		private ThirdPageController _thirdPageController;
		private FourthPageController _fourthPageController;

		public SecondaryWindowController(IBaseWindow view) : base(view) {
			Console.WriteLine("SecondaryWindowController");
			ControllerList.ForEach(Console.WriteLine);
		}

		protected override BaseWindowController GetInstance() { return this; }
		protected override Type GetEnumType() { return typeof(SecondaryWindow.Pages); }
	}
}