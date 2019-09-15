using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LinqToDB.Data;
using Microsoft.Extensions.Logging;

namespace OrchardCoreModule.WebApi.Utils.Database
{
	internal class DataConnectionBase : DataConnection
	{
		private readonly ILogger _logger;
		
		protected DataConnectionBase(string dataProviderName, string connectionString, ILogger logger) 
			: base(dataProviderName, connectionString)
		{
			Console.WriteLine(connectionString);
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}
		
		/// <summary> Execute command and return data reader </summary>
		protected DataReader ExecuteReader(string sql, params DataParameter[] parameters)
		{
			return ExecuteReader(sql, true, parameters);
		}

		/// <summary> Execute command and return data reader </summary>
		protected DataReader ExecuteReader(string sql, bool logEnabled, params DataParameter[] parameters)
		{
			return ExecuteReader(new DbRequest(sql)
			{
				CommandType = CommandType.StoredProcedure,
				Parameters = parameters,
				LogLevel = logEnabled ? LogLevel.Debug : LogLevel.None,
			});
		}

		/// <summary> Execute command and return data reader </summary>
		private DataReader ExecuteReader(DbRequest request)
		{
			using (var scope = new QueryExecutionScope(_logger))
			{
				try
				{
					Command?.Parameters.Clear();
					CommandTimeout = request.CommandTimeout;
				
					return CreateCommand(request).ExecuteReader();
				}
				catch (Exception e)
				{
					scope.Log($"Error {e}");
					throw;
				}
			}
		}
		
		/// <summary> Execute command and return typed list of objects </summary>
		protected IEnumerable<T> Query<T>(string sql, params DataParameter[] parameters)
		{
			return Query<T>(sql, CommandType.StoredProcedure, parameters);
		}

		/// <summary> Execute command and return typed list of objects </summary>
		protected IEnumerable<T> QuerySql<T>(string sql, params DataParameter[] parameters)
		{
			return Query<T>(sql, CommandType.Text, parameters);
		}

		/// <summary> Execute command and return typed list of objects </summary>
		protected IEnumerable<T> Query<T>(string sql, CommandType commandType, params DataParameter[] parameters)
		{
			return Query<T>(sql, commandType, true, parameters);
		}

		/// <summary> Execute command and return typed list of objects </summary>
		protected IEnumerable<T> Query<T>(string sql, CommandType commandType, bool logEnabled, params DataParameter[] parameters)
		{
			return Query<T>(new DbRequest(sql)
			{
				CommandType = commandType,
				Parameters = parameters,
				LogLevel = logEnabled ? LogLevel.Debug : LogLevel.None,
			});
		}

		/// <summary> Execute command and return typed list of objects </summary>
		private IEnumerable<T> Query<T>(DbRequest request)
		{
			using (var scope = new QueryExecutionScope(_logger))
			{
				try
				{
					Command?.Parameters.Clear();
					CommandTimeout = request.CommandTimeout;

					var command = CreateCommand(request);

					return command.Query<T>();
				}
				catch (Exception e)
				{
					scope.Log($"Error {e}");
					throw;
				}
			}
		}
		
		/// <summary> Create execution command </summary>
		private CommandInfo CreateCommand(DbRequest request)
		{
			return new CommandInfo(this, request.CommandText, request.Parameters.ToArray())
			{
				CommandType = request.CommandType,
			};
		}
		
	}
}