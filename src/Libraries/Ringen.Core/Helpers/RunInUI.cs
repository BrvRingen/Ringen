using System;
using System.Windows;
using System.Windows.Threading;

namespace Ringen.Core
{
    public static class RunInUI
    {
        private static Dispatcher m_uiDispatcher = Application.Current?.Dispatcher;

        public static void Run(Action _action)
        {
            if (_action == null)
                return;

            if (m_uiDispatcher?.CheckAccess() == false)
                m_uiDispatcher.Invoke(_action);
            else
                _action();
        }

        public static T Run<T>(Func<T> func)
        {
            if (func == null)
                return default(T);
            // Ab C# 7.1 geht:
            //  return default;

            if (m_uiDispatcher?.CheckAccess() == false)
                return m_uiDispatcher.Invoke(func);
            else
                return func();
        }
    }

}
