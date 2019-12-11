namespace MVC_Test.model {
	public class DataBaseData {
		public static DataBaseData Instance { get; } = new DataBaseData();
		private DataBaseData() { }
	}
}