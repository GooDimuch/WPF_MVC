namespace MVC_Test.model {
	public class DataManager {
		public static DataManager Instance { get; } = new DataManager();
		public RemoteData RemoteData { get; }
		public DataBaseData DataBaseData { get; }

		private DataManager() {
			RemoteData = RemoteData.Instance;
			DataBaseData = DataBaseData.Instance;
		}
	}
}