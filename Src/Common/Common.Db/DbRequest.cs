using System.Collections.Generic;
using System.Data;
using JetBrains.Annotations;
using LinqToDB.Data;
using Microsoft.Extensions.Logging;

namespace Common.Db
{
	[UsedImplicitly]
	internal class DbRequest
	{
		/// <summary> Command text </summary>
		public string CommandText { get; }

		/// <summary> Command type </summary>
		public CommandType CommandType { get; set; } = CommandType.StoredProcedure;

		/// <summary> Command execution timeout (seconds) </summary>
		public int CommandTimeout { get; set; } = 0;

		/// <summary> Command parameters </summary>
		public IEnumerable<DataParameter> Parameters { get; set; } = new List<DataParameter>();

		/// <summary> Level of logging </summary>
		public LogLevel LogLevel { get; set; } = LogLevel.Debug;

		/// <summary> Request constructor </summary>
		/// <param name="commandText"> Command text or stored procedure name to execute </param>
		public DbRequest(string commandText)
		{
			CommandText = commandText;
		}
	}
}