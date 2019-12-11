using System;
using MVC_Test.windows.basePage;
using MVC_Test.windows.baseWindow;

namespace MVC_Test.windows.secondaryWindow.fourthPath {
	/// <summary>
	/// Логика взаимодействия для FourthPage.xaml
	/// </summary>
	public partial class FourthPage : BasePage {
		private FourthPageController Controller => BaseController as FourthPageController;
		private ISecondaryWindow Window => _window as ISecondaryWindow;

		public FourthPage(IBaseWindow window) : base(window) {
			Console.WriteLine("FourthPage");
			InitializeComponent();
		}

		protected override void SetController() { BaseController = new FourthPageController(this); }
	}
}