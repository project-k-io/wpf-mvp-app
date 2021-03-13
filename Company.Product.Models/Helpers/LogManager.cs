using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Company.Product.Models.Helpers
{
    public class LogManager
    {
        private static IServiceProvider _provider;

        public LogManager(IServiceProvider provider)
        {
            _provider = provider;
        }

        public static ILogger GetLogger<T>()
        {
            return _provider?.GetService<ILogger<T>>();
        }
    }
}