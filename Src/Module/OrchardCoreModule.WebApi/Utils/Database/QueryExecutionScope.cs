using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

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

			Log("Started");
		}
		
		public void Dispose()
		{
			_stopwatch.Stop();
			
			Log("Finished");
		}

		public void Log(string message)
		{
			Console.WriteLine($"{_scopeGuid}: {message}");
		}
	}
}