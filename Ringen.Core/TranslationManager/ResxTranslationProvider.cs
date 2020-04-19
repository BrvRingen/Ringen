using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Ringen.Core.TranslationManager
{
    /// <summary>
    /// 
    /// </summary>
    public class ResxTranslationProvider : ITranslationProvider
    {
        #region Private Members

        private Dictionary<string, ResourceManager> _resourceManager;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="ResxTranslationProvider"/> class.
        /// </summary>
        /// <param name="baseName">Name of the base.</param>
        /// <param name="assembly">The assembly.</param>
        public ResxTranslationProvider()
        {
            _resourceManager = new Dictionary<string, ResourceManager>();
        }

        #endregion

        #region ITranslationProvider Members

        /// <summary>
        /// See <see cref="ITranslationProvider.Translate" />
        /// </summary>
        public object Translate(string resource, string key, CultureInfo culture)
        {
            if (resource == null) return null;

            if (!_resourceManager.ContainsKey(resource))
                return null;

            if (culture == null)
                return _resourceManager[resource].GetString(key);
            else
                return _resourceManager[resource].GetString(key, culture);
        }

        #endregion

        #region ITranslationProvider Members

        /// <summary>
        /// See <see cref="ITranslationProvider.AvailableLanguages" />
        /// </summary>
        public IEnumerable<CultureInfo> Languages
        {
            get
            {
                yield return new CultureInfo("de-DE");
                yield return new CultureInfo("en-US");
                yield return new CultureInfo("zh-CN");
            }
        }

        #endregion

        public void AddTranslationResource(string _resource, string _baseName, Assembly _assembly)
        {
            //if (!_resourceManager.ContainsKey(_resource))
                _resourceManager.Add(_resource, new ResourceManager(_baseName, _assembly));
        }
    }
}
