using System.Collections.Generic;
using System.Linq;

namespace Ringen.Core.Services
{
    public static class Service
    {
        #region properties
        public static IPluginService Plugin => ServiceBasic.GetService<IPluginService>();
        public static IRingenService Ringen => ServiceBasic.GetService<IRingenService>();
        public static ILoginService Login => ServiceBasic.GetService<ILoginService>();

        private static bool? m_IsInDesignMode;
        public static bool IsInDesignMode
        {
            get
            {
                if (!m_IsInDesignMode.HasValue)
                {
                    var prop = System.ComponentModel.DesignerProperties.IsInDesignModeProperty;
                    m_IsInDesignMode
                        = (bool)System.ComponentModel.DependencyPropertyDescriptor
                                        .FromProperty(prop, typeof(System.Windows.FrameworkElement))
                                        .Metadata.DefaultValue;
                }
                return m_IsInDesignMode.Value;
            }
        }

        private static bool? m_IsInUnitTestMode;
        private static bool IsInUnitTestMode
        {
            get
            {
                if (!m_IsInUnitTestMode.HasValue)
                {
                    var unitTestAttributes = new HashSet<string>
                    {
                        "Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute",
                        "NUnit.Framework.TestFixtureAttribute",
                    };
                    m_IsInUnitTestMode = false;
                    foreach (var f in new System.Diagnostics.StackTrace().GetFrames())
                        if (f.GetMethod().DeclaringType.GetCustomAttributes(false).Any(x => unitTestAttributes.Contains(x.GetType().FullName)))
                            m_IsInUnitTestMode = true;
                }
                return m_IsInUnitTestMode.Value;
            }
        }

        public static bool IsDebugMode
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }
        #endregion properties

        #region constructors
        static Service()
        {
            if (IsInDesignMode || IsInUnitTestMode)
                return;

            ServiceBasic.Register(typeof(IPluginService), typeof(PluginService));
            ServiceBasic.Register(typeof(ILoginService), typeof(LoginService));
        }
        #endregion constructors

    }
}
