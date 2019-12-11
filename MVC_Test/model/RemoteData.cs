namespace MVC_Test.model {
	public class RemoteData {
		public static RemoteData Instance { get; } = new RemoteData();
		private RemoteData() { }
	}
}