using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using System;

namespace JsonLocalization
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly Lazy<Dictionary<string, string>> _resources;
        private readonly Lazy<Dictionary<string, string>> _fallbackResources;

        public JsonStringLocalizer(string resourceName, Assembly resourceAssembly, CultureInfo cultureInfo)
        {
            _resources = new Lazy<Dictionary<string, string>>(
                () => ReadResources(resourceName, resourceAssembly, cultureInfo, isFallback: false));
            _fallbackResources = new Lazy<Dictionary<string, string>>(
                () => ReadResources(resourceName, resourceAssembly, cultureInfo.Parent, isFallback: true));
        }

        private static Dictionary<string, string> ReadResources(string resourceName, Assembly resourceAssembly, CultureInfo cultureInfo, bool isFallback)
        {
            Assembly satelliteAssembly;
            try
            {
                satelliteAssembly = resourceAssembly.GetSatelliteAssembly(cultureInfo);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return new Dictionary<string, string>();
            }

            var stream = satelliteAssembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                return new Dictionary<string, string>();
            }
            string json;
            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public LocalizedString this[string name]
        {
            get
            {
                if (name == null) throw new ArgumentNullException(nameof(name));
                if (TryGetResource(name, out string value))
                {
                    return new LocalizedString(name, value, resourceNotFound: false);
                }
                return new LocalizedString(name, name, resourceNotFound: true);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                if (name == null) throw new ArgumentNullException(nameof(name));
                if (TryGetResource(name, out string value))
                {
                    return new LocalizedString(name, string.Format(value, arguments), resourceNotFound: false);
                }
                return new LocalizedString(name, string.Format(name, arguments), resourceNotFound: true);
            }
        }

        private bool TryGetResource(string name, out string value)
        {
            return _resources.Value.TryGetValue(name, out value) ||
                _fallbackResources.Value.TryGetValue(name, out value);
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _resources.Value.Select(r => new LocalizedString(r.Key, r.Value));
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            throw new System.NotSupportedException("Obsolete API. See: https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.localization.istringlocalizer.withculture");
        }
    }
}