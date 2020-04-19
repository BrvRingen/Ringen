using System;
using System.Diagnostics;

namespace Ringen.Core.Messaging
{
    public class ResponseMessageBase<T> : MessageBase
    {
        #region declarations

        public Action<T> Callback { get; }

        #endregion

        #region constructors

        public ResponseMessageBase(Action<T> _callback)
        {
            Callback = _callback;
        }

        #endregion

        #region public functions

        public object Execute(params object[] _params)
        {
            if (Callback != null)
                return Callback.DynamicInvoke(_params);

            return false;
        }

        #endregion
    }
}
