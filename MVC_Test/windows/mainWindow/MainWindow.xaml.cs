using System;
using System.Reflection;
using System.Windows;
using MVC_Test.windows.baseWindow;
using MVC_Test.windows.mainWindow.firstPage;
using MVC_Test.windows.mainWindow.secondPage;

namespace MVC_Test.windows.mainWindow {
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : BaseWindow, IMainWindow {
		public enum Pages {
			FirstPage,
			SecondPage,
		}

		private MainWindowController Controller => BaseController as MainWindowController;

		private FirstPage _firstPage;
		private SecondPage _secondPage;

		public MainWindow() : base() {
			Console.WriteLine("MainWindow");
			InitializeComponent();
			
		}

		protected override BaseWindow GetInstance() { return this; }

		protected override void SetController() {
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
			BaseController = new MainWindowController(this);
		}

		private void MainWindow_OnLoadCompleted(object sender, RoutedEventArgs e) {
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
//			Close();
		}

		public override void ChangePage(Enum page) {
			switch (page) {
				case Pages.FirstPage:
					Frame.NavigationService.Navigate(_firstPage);
					break;
				case Pages.SecondPage:
					Frame.NavigationService.Navigate(_secondPage);
					break;
				default: throw new ArgumentOutOfRangeException();
			}
		}

		private void Plus_OnClick(object sender, RoutedEventArgs e) {
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
			Controller.ChangePage(Pages.FirstPage);
			Controller.SetBackgroundForFP();
		}

		private void Minus_OnClick(object sender, RoutedEventArgs e) {
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
			Controller.ChangePage(Pages.SecondPage);
			Controller.SetBackgroundForSP();
		}

		private void Close_OnClick(object sender, RoutedEventArgs e) {
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
			Close();
		}
	}
}