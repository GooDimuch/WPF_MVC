using MVC_Test.windows.basePage;

namespace MVC_Test.windows.secondaryWindow.thirdPage {
	internal class ThirdPageController : BasePageController {
		private IThirdPage View => _view as IThirdPage;
		public ThirdPageController(IBasePage view) : base(view) { }
	}
}