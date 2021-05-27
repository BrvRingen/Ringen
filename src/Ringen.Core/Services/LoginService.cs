using System.ComponentModel;
using System.Security;

namespace Ringen.Core.Services
{
    public sealed class LoginService : ILoginService, INotifyPropertyChanged
    {
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
    }
}
