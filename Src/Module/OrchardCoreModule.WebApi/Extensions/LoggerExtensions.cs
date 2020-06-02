using Microsoft.Extensions.Logging;
using System;

namespace OrchardCoreModule.WebApi.Extensions
{
	internal static class LoggerExtensions
	{
		public static void LogCustom(this ILogger logger, LogLevel logLevel, string message, Exception exception = null)
		{
			logger.Log(logLevel, exception, message);
		}
		
		public static void LogDebug(this ILogger logger, string message)
		{
			logger.Log(LogLevel.Debug, message);
		}
		
		public static void LogError(this ILogger logger, Exception exception, string message)
		{
			logger.Log(LogLevel.Error, exception, message);
		}
	}
}
