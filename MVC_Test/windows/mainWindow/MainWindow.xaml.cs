using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
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

		private const Pages START_PAGE = Pages.FirstPage;
		private Pages CurrentPage = START_PAGE - 1;
		private FirstPage _firstPage;
		private SecondPage _secondPage;

		public MainWindow() : base() {
			Console.WriteLine("MainWindow");
			InitializeComponent();
			SetStartPage();
		}

		private void SetStartPage() => ChangePage(START_PAGE);

		protected override BaseWindow GetInstance() { return this; }

		protected override void SetController() {
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
			BaseController = new MainWindowController(this);
		}

		private void MainWindow_OnLoadCompleted(object sender, RoutedEventArgs e) {
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
			//			Close();
		}

		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
			base.OnMouseLeftButtonDown(e);
			var y = e.GetPosition(this).Y;
			if (y >= 0 && y <= 24) { DragMove(); }
		}

		public override void ChangePage(Enum pageEnum) {
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
			if (!(pageEnum is Pages page) || page == CurrentPage) { return; }
			switch (page) {
				case Pages.FirstPage:
					Frame.NavigationService.Navigate(_firstPage);
					break;
				case Pages.SecondPage:
					Frame.NavigationService.Navigate(_secondPage);
					break;
				default: throw new ArgumentOutOfRangeException();
			}
			CurrentPage = page;
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