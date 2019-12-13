using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using MVC_Test.windows.basePage;
using MVC_Test.windows.baseWindow;

namespace MVC_Test.windows.mainWindow.secondPage {
	/// <summary>
	/// Логика взаимодействия для SecondPage.xaml
	/// </summary>
	public partial class SecondPage : BasePage, ISecondPage {
		private SecondPageController Controller => BaseController as SecondPageController;
		private IMainWindow Window => _window as IMainWindow;

		public SecondPage(IBaseWindow window) : base(window) {
			Console.WriteLine("SecondPage");
			InitializeComponent();
		}

		private void SecondPage_OnLoaded(object sender, RoutedEventArgs e) {
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
		}

		protected override void SetController() {
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
			BaseController = new SecondPageController(this);
		}

		public void SecondPageMethod() {
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
			Background = Brushes.DarkRed;
		}
	}
}