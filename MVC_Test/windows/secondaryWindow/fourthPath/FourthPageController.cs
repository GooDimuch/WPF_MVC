using MVC_Test.windows.basePage;

namespace MVC_Test.windows.secondaryWindow.fourthPath {
	internal class FourthPageController : BasePageController {
		private IFourthPage View => _view as IFourthPage;
		public FourthPageController(IBasePage view) : base(view) { }
	}
}