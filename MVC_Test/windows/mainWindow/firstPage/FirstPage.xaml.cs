using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using MVC_Test.windows.basePage;
using MVC_Test.windows.baseWindow;

namespace MVC_Test.windows.mainWindow.firstPage {
	/// <summary>
	/// Логика взаимодействия для FirstPage.xaml
	/// </summary>
	public partial class FirstPage : BasePage, IFirstPage {
		private FirstPageController Controller => BaseController as FirstPageController;
		private IMainWindow Window => _window as IMainWindow;

		public FirstPage(IBaseWindow window) : base(window) {
			Console.WriteLine("FirstPage");
			InitializeComponent();
		}

		private void FirstPage_OnLoaded(object sender, RoutedEventArgs e) {
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
		}

		protected override void SetController() {
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
			BaseController = new FirstPageController(this);
		}

		public void FirstPageMethod() {
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
			Background = Brushes.DarkGreen;
			//
		}
	}
}