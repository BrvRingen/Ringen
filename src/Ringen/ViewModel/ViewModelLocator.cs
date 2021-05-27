using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Ringen.ViewModel
{
    public class ViewModelLocator : DynamicObject
    {
        private static readonly Dictionary<string, Type> m_ViewModels = new Dictionary<string, Type>();

        static ViewModelLocator()
        {
            RegisterViewModel(nameof(MainViewModel), typeof(MainViewModel));

            //RegisterViewModel(nameof(ViewModel.LoggerViewModel), typeof(ViewModel.LoggerViewModel));
            //RegisterViewModel(nameof(ViewModel.ConsoleViewModel), typeof(ViewModel.ConsoleViewModel));
        }

        public override IEnumerable<string> GetDynamicMemberNames() =>
            m_ViewModels.Keys;

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return GetViewModel(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (m_ViewModels.ContainsKey(binder.Name))
            {
                m_ViewModels[binder.Name] = value as Type;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void RegisterViewModel(string name, Type viewModel)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            var ioc = SimpleIoc.Default;
            var iocType = typeof(SimpleIoc);
            var methodInfo = iocType.GetMethods().Single(
                m =>
                    m.Name == "Register" &&
                    m.GetGenericArguments().Length == 1 &&
                    m.GetParameters().Length == 0);

            var genericArguments = new Type[] { viewModel };

            var genericMethodInfo = methodInfo.MakeGenericMethod(genericArguments);

            genericMethodInfo.Invoke(ioc, new object[0]);


            m_ViewModels.Add(name, viewModel);
        }

        public static bool GetViewModel(string name, out object result)
        {
            if (m_ViewModels.ContainsKey(name))
            {
                var resultType = m_ViewModels[name];
                result = SimpleIoc.Default.GetInstance(resultType);
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public static T GetViewModel<T>(string name) where T : class
        {
            if (GetViewModel(name, out object result))
            {
                return result as T;
            }
            return default(T);
        }
    }
}
