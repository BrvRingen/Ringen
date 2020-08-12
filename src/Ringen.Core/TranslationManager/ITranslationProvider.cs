using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Ringen.Core.TranslationManager
{
    public interface ITranslationProvider
    {
        /// <summary>
        /// Add Tanslation Resource
        /// </summary>
        /// <param name="_baseName">Resource Name</param>
        /// <param name="_assembly">Assembly whrere the resource is located</param>
        void AddTranslationResource(string _resource, string _baseName, Assembly _assembly);

        /// <summary>
        /// Translates the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        object Translate(string resource, string key, CultureInfo culture);

        /// <summary>
        /// Gets the available languages.
        /// </summary>
        /// <value>The available languages.</value>
        IEnumerable<CultureInfo> Languages { get; }

    }
}
