using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Ringen.Core.TranslationManager
{
    public class TransManager
    {
        #region declarations

        private static TransManager _translationManager;

        public event EventHandler LanguageChanged;

        #endregion

        #region constructor

        public TransManager()
        {
            TranslationProvider = new ResxTranslationProvider();
        }

        #endregion

        #region properties

        public CultureInfo CurrentLanguage
        {
            get { return Thread.CurrentThread.CurrentUICulture; }
            set
            {
                if (value != Thread.CurrentThread.CurrentUICulture)
                {
                    Thread.CurrentThread.CurrentUICulture = value;
                    OnLanguageChanged();
                }
            }
        }

        public IEnumerable<CultureInfo> Languages
        {
            get
            {
                if (TranslationProvider != null)
                {
                    return TranslationProvider.Languages;
                }
                return Enumerable.Empty<CultureInfo>();
            }
        }

        public static TransManager Instance
        {
            get
            {
                if (_translationManager == null)
                    _translationManager = new TransManager();
                return _translationManager;
            }
        }

        public ITranslationProvider TranslationProvider { get; set; }

        #endregion

        #region events

        private void OnLanguageChanged()
        {
            LanguageChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region public functions

        public void AddTranslationResource(object _resource, string _baseName, Assembly _assembly)
        {
            Instance.TranslationProvider.AddTranslationResource(_resource.ToString(), _baseName, _assembly);
        }

        public object Translate(object resource, object key, CultureInfo culture)
        {
            if (resource == null || key == null)
                return "Translation Error - resource or key is null.";

            if (Instance.TranslationProvider != null)
            {
                object translatedValue = Instance.TranslationProvider.Translate(resource.ToString(), key.ToString(), culture);

                if (translatedValue != null)
                {
                    return translatedValue;
                }
            }

            return string.Format("!{0}!", key);
        }

        #endregion

        #region static functions
        public static string TranslateString(string resource, string key, CultureInfo culture = null) { return Instance.Translate(resource, key, culture).ToString().Replace(@"\n", Environment.NewLine); }
        public static string TranslateString(Enum key, CultureInfo culture = null) { return Instance.Translate(key.GetType().FullName.Replace(".", "_"), key, culture).ToString().Replace(@"\n", Environment.NewLine); }

        public static string TranslateString(string resource, string key, object arg0, CultureInfo culture = null) { return string.Format(Instance.Translate(resource, key, culture).ToString(), arg0).Replace(@"\n", Environment.NewLine); }
        public static string TranslateString(Enum key, object arg0, CultureInfo culture = null) { return string.Format(Instance.Translate(key.GetType().FullName.Replace(".", "_"), key, culture).ToString(), arg0).Replace(@"\n", Environment.NewLine); }

        public static string TranslateString(string resource, string key, object arg0, object arg1, CultureInfo culture = null) { return string.Format(Instance.Translate(resource, key, culture).ToString(), arg0, arg1).Replace(@"\n", Environment.NewLine); }
        public static string TranslateString(Enum key, object arg0, object arg1, CultureInfo culture = null) { return string.Format(Instance.Translate(key.GetType().FullName.Replace(".", "_"), key, culture).ToString(), arg0, arg1).Replace(@"\n", Environment.NewLine); }

        public static string TranslateString(string resource, string key, object arg0, object arg1, object arg2, CultureInfo culture = null) { return string.Format(Instance.Translate(resource, key, culture).ToString(), arg0, arg1, arg2).Replace(@"\n", Environment.NewLine); }
        public static string TranslateString(Enum key, object arg0, object arg1, object arg2, CultureInfo culture = null) { return string.Format(Instance.Translate(key.GetType().FullName.Replace(".", "_"), key, culture).ToString(), arg0, arg1, arg2).Replace(@"\n", Environment.NewLine); }

        public static string TranslateString(string resource, string key, params object[] args) { return string.Format(Instance.Translate(resource, key, null).ToString(), args).Replace(@"\n", Environment.NewLine); }
        public static string TranslateString(Enum key, params object[] args) { return string.Format(Instance.Translate(key.GetType().FullName.Replace(".", "_"), key, null).ToString(), args).Replace(@"\n", Environment.NewLine); }

        public static string TranslateString(string resource, string key, CultureInfo culture = null, params object[] args) { return string.Format(Instance.Translate(resource, key, culture).ToString(), args).Replace(@"\n", Environment.NewLine); }
        public static string TranslateString(Enum key, CultureInfo culture = null, params object[] args) { return string.Format(Instance.Translate(key.GetType().FullName.Replace(".", "_"), key, culture).ToString(), args).Replace(@"\n", Environment.NewLine); }

        #endregion
    }
}
