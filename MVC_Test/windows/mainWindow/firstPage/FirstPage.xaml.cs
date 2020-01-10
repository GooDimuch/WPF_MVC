using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using MVC_Test.customViews.dropBoxMissions;
using MVC_Test.customViews.dropBoxWithCategories;
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
			DropBox.Source = Controller.GetDropBoxModel();
			DropBoxMission.Source = Controller.GetDropBoxMissionModel();
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

		private void DropBox_OnMouseDown(object sender, RoutedEventArgs e) {
			Console.WriteLine("startMission");
			Console.WriteLine(sender.GetType());
			Console.WriteLine((sender as DropBoxWithCategories).KeyList);
		}

		private void DropBoxMissions_OnMouseDown(object sender, RoutedEventArgs e) {
			Console.WriteLine("startMission");
			Console.WriteLine(sender.GetType());
			Console.WriteLine((sender as DropBoxMissions).KeyList);
		}
	}
}