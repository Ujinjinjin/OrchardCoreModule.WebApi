using LinqToDB.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OrchardCoreModule.WebApi.Utils.Database
{
	internal class DataConnectionBase : DataConnection
	{
		private readonly ILogger _logger;
		
		protected DataConnectionBase(
			string dataProviderName,
			string connectionString,
			ILogger logger
		) : base(dataProviderName, connectionString)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}
		
		/// <summary> Execute sql returning nothing </summary>
		protected async Task ExecuteAsync(string sql, params DataParameter[] parameters)
		{
			await ExecuteAsync(sql, CommandType.StoredProcedure, true, parameters);
		}
		
		/// <summary> Execute sql returning nothing </summary>
		protected async Task ExecuteSqlAsync(string sql, params DataParameter[] parameters)
		{
			await ExecuteAsync(sql, CommandType.Text, true, parameters);
		}
		
		/// <summary> Execute sql returning nothing </summary>
		protected async Task ExecuteAsync(string sql, CommandType commandType, bool logEnabled, params DataParameter[] parameters)
		{
			await ExecuteAsync(new DbRequest(sql)
			{
				CommandType = commandType,
				Parameters = parameters,
				LogLevel = logEnabled ? LogLevel.Debug : LogLevel.None,
			});
		}

		/// <summary> Execute sql returning nothing </summary>
		protected async Task ExecuteAsync(DbRequest request)
		{
			using (var scope = new QueryExecutionScope(_logger))
			{
				try
				{
					Command?.Parameters.Clear();
					CommandTimeout = request.CommandTimeout;
				
					await CreateCommand(request).ExecuteAsync();
				}
				catch (Exception exception)
				{
					scope.Log(LogLevel.Error, "DataConnectionBase:Error;", exception);
					throw;
				}
			}
		}
		
		/// <summary> Execute command and return data reader </summary>
		protected async Task<DataReaderAsync> ExecuteReaderAsync(string sql, params DataParameter[] parameters)
		{
			return await ExecuteReaderAsync(sql, CommandType.StoredProcedure, true, parameters);
		}
		
		/// <summary> Execute command and return data reader </summary>
		protected async Task<DataReaderAsync> ExecuteReaderSqlAsync(string sql, params DataParameter[] parameters)
		{
			return await ExecuteReaderAsync(sql, CommandType.Text, true, parameters);
		}

		/// <summary> Execute command and return data reader </summary>
		protected async Task<DataReaderAsync> ExecuteReaderAsync(string sql, CommandType commandType, bool logEnabled, params DataParameter[] parameters)
		{
			return await ExecuteReaderAsync(new DbRequest(sql)
			{
				CommandType = commandType,
				Parameters = parameters,
				LogLevel = logEnabled ? LogLevel.Debug : LogLevel.None,
			});
		}

		/// <summary> Execute command and return data reader </summary>
		private async Task<DataReaderAsync> ExecuteReaderAsync(DbRequest request)
		{
			using (var scope = new QueryExecutionScope(_logger))
			{
				try
				{
					Command?.Parameters.Clear();
					CommandTimeout = request.CommandTimeout;
				
					return await CreateCommand(request).ExecuteReaderAsync();
				}
				catch (Exception exception)
				{
					scope.Log(LogLevel.Error, "DataConnectionBase:Error;", exception);
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
		protected Task<IEnumerable<T>> QueryAsync<T>(string sql, params DataParameter[] parameters)
		{
			return Task.FromResult(Query<T>(sql, parameters));
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
				catch (Exception exception)
				{
					scope.Log(LogLevel.Error, "DataConnectionBase:Error;", exception);
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