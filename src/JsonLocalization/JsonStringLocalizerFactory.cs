using System;
using System.Reflection;
using System.Globalization;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using Microsoft.Extensions.Localization;

namespace JsonLocalization
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly ConcurrentDictionary<string, IStringLocalizer> _cache = new ConcurrentDictionary<string, IStringLocalizer>();

        public JsonStringLocalizerFactory()
        {
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            var resourceType = resourceSource.GetTypeInfo();
            var cultureInfo = CultureInfo.CurrentUICulture;
            var resourceName = $"{resourceType.FullName}.json";

            return GetCachedLocalizer(resourceName, resourceType.Assembly, cultureInfo);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            var resourceName = $"{baseName}.json";

            var cultureInfo = CultureInfo.CurrentUICulture;
            return GetCachedLocalizer(resourceName, Assembly.GetCallingAssembly(), cultureInfo);
        }

        private IStringLocalizer GetCachedLocalizer(string resourceName, Assembly assembly, CultureInfo cultureInfo)
        {
            string cacheKey = GetCacheKey(resourceName, assembly, cultureInfo);

            return _cache.GetOrAdd(cacheKey, new JsonStringLocalizer(resourceName, assembly, cultureInfo));
        }

        private string GetCacheKey(string resourceName, Assembly assembly, CultureInfo cultureInfo)
        {
            return resourceName + ';' + assembly.FullName + ';' + cultureInfo.Name;
        }
    }
}