using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MVC_Test.windows.basePage;

namespace MVC_Test.windows.baseWindow {
	public abstract class BaseWindowController {
		protected List<BasePageController> ControllerList { get; private set; }
		protected readonly IBaseWindow _view;

		protected BaseWindowController(IBaseWindow view) {
			Console.WriteLine("BaseWindowController");
			_view = view;
			SetControllerList();
			InitializeAllPageControllers();
		}

		protected abstract BaseWindowController GetInstance();

		protected abstract Type GetEnumType();

		protected void InitializeAllPageControllers() {
			foreach (var fieldInfo in GetInstance()
															.GetType()
															.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
															.Where(info => info.Name.EndsWith("PageController"))) {
				fieldInfo.SetValue(GetInstance(),
													ControllerList.Find(controller =>
																								fieldInfo.Name.Contains(controller.GetType().Name,
																																				StringComparison.OrdinalIgnoreCase)));
			}
		}

		protected void SetControllerList() {
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
			ControllerList = BasePageController.ControllerList.Where(CheckOwnPage).ToList();
		}

		private bool CheckOwnPage(BasePageController controller) {
			return Enum.GetValues(GetEnumType())
								.Cast<object>()
								.Any(page => controller.GetType().Name.Contains(page.ToString()));
		}
	}
}