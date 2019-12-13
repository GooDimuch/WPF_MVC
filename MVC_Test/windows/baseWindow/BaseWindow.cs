using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using MVC_Test.windows.basePage;

namespace MVC_Test.windows.baseWindow {
	public abstract class BaseWindow : Window, IBaseWindow {
		protected BaseWindowController BaseController;

		protected BaseWindow() {
			Console.WriteLine("BaseWindow");
			InitializeAllPages();
			SetController();
		}

		protected abstract BaseWindow GetInstance();

		private void InitializeAllPages() {
			foreach (var fieldInfo in GetInstance()
															.GetType()
															.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
															.Where(info => info.FieldType.BaseType == typeof(BasePage))
															.Where(info => info.Name.EndsWith("Page"))) {
				fieldInfo.SetValue(GetInstance(), Activator.CreateInstance(fieldInfo.FieldType, this));
			}
		}

		protected abstract void SetController();
		public abstract void ChangePage(Enum page);
	}
}