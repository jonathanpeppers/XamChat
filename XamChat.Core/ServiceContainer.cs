using System;
using System.Collections.Generic;

namespace XamChat.Core
{
	public static class ServiceContainer
	{
		static readonly Dictionary<Type, Lazy<object>> services = new Dictionary<Type, Lazy<object>>();

		public static void Register<T>(Func<T> function)
		{
			services[typeof(T)] = new Lazy<object>(() => function());
		}

		public static T Resolve<T>()
		{
			return (T)Resolve(typeof(T));
		}

		public static object Resolve(Type type)
		{
			Lazy<object> service;
			if (services.TryGetValue(type, out service))
			{
				return service.Value;
			}
			throw new Exception("Service not found!");
		}
	}
}

