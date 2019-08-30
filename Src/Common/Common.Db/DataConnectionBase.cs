using System;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using Microsoft.Extensions.Logging;

namespace Common.Db
{
	public class DataConnectionBase : DataConnection
	{
		private readonly ILogger _logger;
		
		public bool ValidateRecordsBeforeInsertOrUpdate { get; set; }

		static DataConnectionBase()
		{
			LinqToDB.Mapping.MappingSchema.Default.SetConvertExpression<DateTime, DateTime>(dt => DateTime.SpecifyKind(dt, DateTimeKind.Utc));
		}

		protected DataConnectionBase(string connectionString, ILogger logger)
			: base(SqlServerTools.GetDataProvider(SqlServerVersion.v2017), connectionString)
		{
			_logger = logger;
		}
		
		
	}
}