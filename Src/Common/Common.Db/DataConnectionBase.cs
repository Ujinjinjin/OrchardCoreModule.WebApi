using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Db.Abstractions;
using Common.Db.Settings;
using LinqToDB.Data;
using Microsoft.Extensions.Logging;

namespace Common.Db
{
	public class DataConnectionBase : DataConnection
	{
		private readonly ILogger _logger;
		
		protected DataConnectionBase(DatabaseType databaseType, string connectionString, ILogger logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			
			switch (databaseType)
			{
				case DatabaseType.SqlServer:
					DefaultSettings = new SqlServerConnectionSettings(connectionString);
					break;
				case DatabaseType.Postgres:
				case DatabaseType.MySql:
				case DatabaseType.SqLite:
				default:
					throw new ArgumentOutOfRangeException(nameof(databaseType), databaseType, null);
			}
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
					scope.Log("Error");
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
					scope.Log("Error");
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