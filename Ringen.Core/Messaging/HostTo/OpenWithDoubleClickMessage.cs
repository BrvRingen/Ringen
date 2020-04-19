using GalaSoft.MvvmLight.Messaging;
using System;
using Ringen.Core.UI;

namespace Ringen.Core.Messaging
{
    public class OpenWithDoubleClickMessage : ResponseMessageBase<(IRingenTabItem RingenTabItem, bool Open)>
    {
        #region declarations

        #endregion

        #region constructors

        private OpenWithDoubleClickMessage(Action<(IRingenTabItem RingenTabItem, bool Open)> _callback) : base(_callback)
        {
        }

        #endregion

        #region methods
        public static void Send(Action<(IRingenTabItem RingenTabItem, bool Open)> _callback)
        {
            Messenger.Default.Send(new OpenWithDoubleClickMessage(_callback));
        }
        #endregion methods
    }
}
