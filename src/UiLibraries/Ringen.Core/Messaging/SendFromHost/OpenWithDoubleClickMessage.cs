using System;
using GalaSoft.MvvmLight.Messaging;
using Ringen.Core.UI;

namespace Ringen.Core.Messaging
{
    public class OpenWithDoubleClickMessage : ResponseMessageBase<(IRingenTabItem RingenTabItem, bool Open)>
    {
        private OpenWithDoubleClickMessage(Action<(IRingenTabItem RingenTabItem, bool Open)> _callback) : base(_callback)
        {
        }

        public static void Send(Action<(IRingenTabItem RingenTabItem, bool Open)> _callback)
        {
            Messenger.Default.Send(new OpenWithDoubleClickMessage(_callback));
        }
    }
}
