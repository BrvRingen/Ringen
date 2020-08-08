using Ringen.Core.UI;
using Ringen.Core.PluginSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Linq;
using Ringen.Core.Messaging;
using System.ComponentModel;
using System.Security;

namespace Ringen.Core.Services
{
    public sealed class LoginService : ILoginService, INotifyPropertyChanged
    {
        #region properties

        public event PropertyChangedEventHandler PropertyChanged;

        private string m_UserName;
        public string UserName
        {
            get
            {
                return m_UserName;
            }
            set
            {
                if (m_UserName != value)
                {
                    m_UserName = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserName"));
                }
            }
        }

        private SecureString m_Password;
        public SecureString Password
        {
            get
            {
                return m_Password;
            }
            set
            {
                if (m_Password != value)
                {
                    m_Password = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
                }
            }

        }

        #endregion

        #region public functions

        #endregion
    }
}
