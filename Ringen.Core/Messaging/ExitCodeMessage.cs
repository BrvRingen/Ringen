using GalaSoft.MvvmLight.Messaging;

namespace Ringen.Core.Messaging
{
    public class ExitCodeMessage : MessageBase
    {
        #region declarations

        public int ExitCode { get; }

        #endregion

        #region constructors

        private ExitCodeMessage(int _exitCode)
        {
            ExitCode = _exitCode;
        }

        #endregion

        #region methods

        public static void Send(int _exitCode)
        {
            Messenger.Default.Send(new ExitCodeMessage(_exitCode));
        }

        #endregion
    }
}
