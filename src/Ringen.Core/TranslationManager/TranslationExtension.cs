using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Ringen.Core.TranslationManager
{
    /// <summary>
    /// The Translate Markup extension returns a binding to a TranslationData
    /// that provides a translated resource of the specified key
    /// </summary>
    public class TranslateExtension : MarkupExtension
    {
        private static List<string> designModeLoaded = new List<string>();

        #region Private Members

        private string _resource;
        private string _key;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateExtension"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public TranslateExtension()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateExtension"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public TranslateExtension(string resource, string key)
        {
            this._resource = resource;
            this._key = key;
        }

        #endregion

        [ConstructorArgument("Resource")]
        public string Resource
        {
            get { return _resource; }
            set { _resource = value; }
        }

        [ConstructorArgument("Key")]
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        /// <summary>
        /// See <see cref="MarkupExtension.ProvideValue" />
        /// </summary>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return GetBinding(Resource, Key).ProvideValue(serviceProvider);
        }

        static Binding GetBinding(string _resource, string _key)
        {
            var binding = new Binding("Value")
            {
                Source = new TranslationData(_resource, _key)
            };

            return binding;
        }

        public static void SetBinding(DependencyObject target, DependencyProperty dp, object _resource, object _key)
        {
            BindingOperations.SetBinding(target, dp, GetBinding(_resource.ToString(), _key.ToString()));
        }
    }
}
