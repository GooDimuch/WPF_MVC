using System;
using MVC_Test.windows.basePage;
using MVC_Test.windows.baseWindow;

namespace MVC_Test.windows.secondaryWindow.thirdPage {
	/// <summary>
	/// Логика взаимодействия для ThirdPage.xaml
	/// </summary>
	public partial class ThirdPage : BasePage {
		private ThirdPageController Controller => BaseController as ThirdPageController;
		private ISecondaryWindow Window => _window as ISecondaryWindow;

		public ThirdPage(IBaseWindow window) : base(window) {
			Console.WriteLine("ThirdPage");
			InitializeComponent();
		}

		protected override void SetController() { BaseController = new ThirdPageController(this); }
	}
}