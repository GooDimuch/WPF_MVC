using System;
using MVC_Test.windows.baseWindow;
using MVC_Test.windows.mainWindow.firstPage;
using MVC_Test.windows.mainWindow.secondPage;

namespace MVC_Test.windows.mainWindow {
	public class MainWindowController : BaseWindowController {
		private IMainWindow View => _view as IMainWindow;

		private FirstPageController _firstPageController;
		private SecondPageController _secondPageController;

		public MainWindowController(IBaseWindow view) : base(view) {
			Console.WriteLine("MainWindowController");
			ControllerList.ForEach(Console.WriteLine);
		}

		protected override BaseWindowController GetInstance() { return this; }
		protected override Type GetEnumType() { return typeof(MainWindow.Pages); }

		public void ChangePage(MainWindow.Pages page) { View.ChangePage(page); }
		public void SetBackgroundForFP() { _firstPageController.SetBackground(); }
		public void SetBackgroundForSP() { _secondPageController.SetBackground(); }
	}
}