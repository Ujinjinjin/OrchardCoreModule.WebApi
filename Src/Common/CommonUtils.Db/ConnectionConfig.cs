using System;
using CommonUtils.Db.Abstractions;
using LinqToDB.Configuration;
using LinqToDB.Data;

namespace CommonUtils.Db
{
	/// <summary> Data Base connection configurator </summary>
	public static class ConnectionConfig
	{
		/// <summary> Configure default connection settings </summary>
		public static void ConfigureDefaultSettings(DataProvider dataProvider)
		{
			ILinqToDBSettings connectionSettings;

			switch (dataProvider)
			{
				case DataProvider.SqlServer:
					connectionSettings = new SqlServerConnectionSettings();
					break;
				case DataProvider.Postgres:
				case DataProvider.MySql:
				case DataProvider.SqLite:
				default:
					throw new ArgumentOutOfRangeException(nameof(dataProvider), dataProvider, null);
			}
			DataConnection.DefaultSettings = connectionSettings;
		}
	}
}