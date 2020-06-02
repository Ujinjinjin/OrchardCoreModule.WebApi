using Microsoft.Extensions.Logging;
using OrchardCoreModule.WebApi.Extensions;
using System;
using System.Diagnostics;

namespace OrchardCoreModule.WebApi.Utils.Database
{
	internal class QueryExecutionScope : IDisposable
	{
		private readonly Stopwatch _stopwatch;
		private readonly ILogger _logger;
		private readonly string _scopeGuid;
		
		public QueryExecutionScope(ILogger logger)
		{
			_stopwatch = Stopwatch.StartNew();
			_scopeGuid = Guid.NewGuid().ToString("N");
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}
		
		public void Dispose()
		{
			_stopwatch.Stop();
		}

		public void Log(LogLevel logLevel, string message, Exception exception = null)
		{
			_logger.LogCustom(logLevel, $"{_scopeGuid}: {message}", exception);
		}
	}
}