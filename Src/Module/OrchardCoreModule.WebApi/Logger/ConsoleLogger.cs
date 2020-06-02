using Microsoft.Extensions.Logging;
using OrchardCoreModule.WebApi.Helpers;
using System;

namespace OrchardCoreModule.WebApi.Logger
{
	internal class ConsoleLogger : ILogger
	{
		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			if (!IsEnabled(logLevel))
			{
				return;
			}

			if (formatter == null)
			{
				throw new ArgumentNullException(nameof(formatter));
			}

			var message = formatter(state, exception);
			if (exception != null)
			{
				message += Environment.NewLine + Environment.NewLine + exception;
			}

			if (string.IsNullOrEmpty(message))
			{
				return;
			}

			message = $"{logLevel} | {DateTime.UtcNow} | {message}";
			
			Console.WriteLine(message);
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return logLevel != LogLevel.None;
		}

		public IDisposable BeginScope<TState>(TState state)
		{
			return DisposableHelper.Empty;
		}
	}
}
