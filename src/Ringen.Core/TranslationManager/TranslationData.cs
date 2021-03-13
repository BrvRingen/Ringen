using System;
using System.ComponentModel;
using System.Windows;

namespace Ringen.Core.TranslationManager
{
    public class TranslationData : IWeakEventListener, INotifyPropertyChanged
    {
        private string _resource;
        private string _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationData"/> class.
        /// </summary>
        /// <param name="resource">The Resource.</param>
        /// <param name="key">The key.</param>
        public TranslationData(string resource, string key)
        {
            _resource = resource;
            _key = key;
            LanguageChangedEventManager.AddListener(TransManager.Instance, this);
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="TranslationData"/> is reclaimed by garbage collection.
        /// </summary>
        ~TranslationData()
        {
            LanguageChangedEventManager.RemoveListener(TransManager.Instance, this);
        }

        public object Value
        {
            get
            {
                return TransManager.Instance.Translate(_resource, _key, null);
            }
        }

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(LanguageChangedEventManager))
            {
                OnLanguageChanged(sender, e);
                return true;
            }
            return false;
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
