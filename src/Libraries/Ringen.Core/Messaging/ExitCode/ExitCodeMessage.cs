using GalaSoft.MvvmLight.Messaging;

namespace Ringen.Core.Messaging
{
    public class ExitCodeMessage : MessageBase
    {
        public int ExitCode { get; }

        private ExitCodeMessage(int _exitCode)
        {
            ExitCode = _exitCode;
        }

        public static void Send(int _exitCode)
        {
            Messenger.Default.Send(new ExitCodeMessage(_exitCode));
        }
    }
}
