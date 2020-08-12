using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Ringen.Core.UI
{
    public abstract class ExtendedNotifyPropertyChangedUserControl : UserControl, INotifyPropertyChanged
    {
        #region declarations

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region public functions

        /// <summary>
        /// Returns the value of the parsed XmlAttribute.
        /// </summary>
        /// <typeparam name="T">Type of the returned value.</typeparam>
        /// <param name="element">The XmlElement.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns>Converted value</returns>
        protected static T Get<T>(JToken element)
        {
            if (element == null)
                return default;

            string tempVal = element.ToString();

            if (!string.IsNullOrWhiteSpace(tempVal))
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(null, CultureInfo.InvariantCulture, tempVal);
            else
                return default;
        }

        /// <summary>
        /// Helper method for changing a field. 
        /// If the previous field differs from the new value, 
        /// an event will be raised.
        /// </summary>
        /// <typeparam name="T">Type of the field.</typeparam>
        /// <param name="field">Reference to the field.</param>
        /// <param name="value">New value of the field.</param>
        /// <param name="propertyName">Optional name of the property. If not used: it takes the Caller Member Name.</param>
        /// <returns>True if the field has changed.</returns>
        public bool Set<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        public bool Set<T>(ref JObject data, string field, T value, [CallerMemberName]string propertyName = null)
        {
            data[field] = value.ToString();
            OnPropertyChanged(propertyName);

            return true;
        }

        public void OnPropertyChanged([CallerMemberName] string _property = "") //, bool _doNotValidate = false
        {
            //validate the property name in debug builds
//#if DEBUG && !_doNotValidate
//                VerifyProperty(Assembly.GetCallingAssembly()..GetType(), _property);
//#endif

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_property));
        }

        #endregion

        #region internal functions
        /*
        /// <summary>
        /// Verifies whether the current class provides a property with a given
        /// name. This method is only invoked in debug builds, and results in
        /// a runtime exception if the <see cref="OnPropertyChanged"/> method
        /// is being invoked with an invalid property name. This may happen if
        /// a property's name was changed but not the parameter of the property's
        /// invocation of <see cref="OnPropertyChanged"/>.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), Conditional("DEBUG")]
        [DebuggerNonUserCode]
        private void VerifyProperty(Type type, string propertyName)
        {
            //look for a *public* property with the specified name
            PropertyInfo pi = type.GetProperty(propertyName);
            if (pi == null)
            {
                //there is no matching property - notify the developer
                string msg = "OnPropertyChanged was invoked with invalid property name {0}: ";
                msg += "{0} is not a public property of {1}.";
                msg = string.Format(msg, propertyName, type.FullName);
                Debug.Fail(msg);
            }
        }
        */
        #endregion
    }
}
