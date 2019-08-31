using LinqToDB.Configuration;

namespace CommonUtils.Db
{
	internal class ConnectionStringSetting : IConnectionStringSettings
	{
		public string ConnectionString { get; set; }
		public string Name { get; set; }
		public string ProviderName { get; set; }
		public bool IsGlobal => false;
	}
}