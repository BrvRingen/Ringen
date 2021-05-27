using System;

namespace Ringen.Core.Messaging
{
    public class ResponseMessageBase<T> : MessageBase
    {
        public Action<T> Callback { get; }

        public ResponseMessageBase(Action<T> _callback)
        {
            Callback = _callback;
        }

        public object Execute(params object[] _params)
        {
            if (Callback != null)
                return Callback.DynamicInvoke(_params);

            return false;
        }
    }
}
