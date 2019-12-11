using System;
using System.Windows.Controls;
using MVC_Test.windows.baseWindow;

namespace MVC_Test.windows.basePage {
	public abstract class BasePage : Page, IBasePage {
		protected BasePageController BaseController;
		protected IBaseWindow _window;

		protected BasePage(IBaseWindow window) {
			Console.WriteLine("BasePage");
			_window = window;
			SetController();
		}

		protected abstract void SetController();
	}
}