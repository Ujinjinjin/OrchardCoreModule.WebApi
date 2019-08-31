namespace CommonUtils.Db.Abstractions
{
	/// <summary> Data provider </summary>
	public enum DataProvider
	{
		/// <summary> Sql Server </summary>
		SqlServer = 0,

		/// <summary> Postgres </summary>
		Postgres = 1,

		/// <summary> MySQL </summary>
		MySql = 2,

		/// <summary> SQLite </summary>
		SqLite = 3
	}
}