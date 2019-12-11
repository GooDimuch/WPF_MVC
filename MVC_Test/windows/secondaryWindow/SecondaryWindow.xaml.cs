using System;
using MVC_Test.windows.baseWindow;
using MVC_Test.windows.secondaryWindow.fourthPath;
using MVC_Test.windows.secondaryWindow.thirdPage;

namespace MVC_Test.windows.secondaryWindow {
	/// <summary>
	/// Логика взаимодействия для SecondaryWindow.xaml
	/// </summary>
	public partial class SecondaryWindow : BaseWindow {
		private SecondaryWindowController Controller => BaseController as SecondaryWindowController;

		public enum Pages {
			ThirdPage,
			FourthPage,
		}

		private ThirdPage _thirdPage;
		private FourthPage _fourthPage;

		public SecondaryWindow() {
			Console.WriteLine("SecondaryWindow");
			InitializeComponent();
		}

		protected override BaseWindow GetInstance() { return this; }
		protected override void SetController() { BaseController = new SecondaryWindowController(this); }

		public override void ChangePage(Enum page) {
			switch (page) {
				case Pages.ThirdPage:
					Frame.NavigationService.Navigate(_thirdPage);
					break;
				case Pages.FourthPage:
					Frame.NavigationService.Navigate(_fourthPage);
					break;
				default: throw new ArgumentOutOfRangeException();
			}
		}
	}
}