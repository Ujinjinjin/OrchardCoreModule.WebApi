using System;

namespace OrchardCoreModule.WebApi.Helpers
{
	/// <summary> Allows to execute code in using scope </summary>
	internal static class DisposableHelper
	{
		/// <summary> Empty disposable object </summary>
		public static readonly IDisposable Empty = Create(() => {});

		private class DisposableAction : IDisposable
		{
			private readonly Action _action;

			public DisposableAction(Action action)
			{
				_action = action ?? throw new ArgumentNullException(nameof(action));
			}

			public void Dispose()
			{
				_action();
			}
		}

		/// <summary> Creates IDisposable, executing action </summary>
		public static IDisposable Create(Action action)
		{
			return new DisposableAction(action);
		}
	}
}
