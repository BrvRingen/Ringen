using Ringen.Core.UI;
using Ringen.Core.PluginSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Linq;
using Ringen.Core.Messaging;

namespace Ringen.Core.Services
{
    public sealed class PluginService : IPluginService
    {
        private readonly Dictionary<Type, IPlugabble> m_RegisteredPlugins = new Dictionary<Type, IPlugabble>();
        //private IFrameNavigationService m_FrameNavigationService;

        IReadOnlyList<IButton> IPluginService.RegisteredButtons => RegisteredButtons;
        public ObservableCollection<IButton> RegisteredButtons { get; } = new ObservableCollection<IButton>();
        IReadOnlyList<IButton> IPluginService.RegisteredMenuButtons => RegisteredMenuButtons;
        public ObservableCollection<IButton> RegisteredMenuButtons { get; } = new ObservableCollection<IButton>();

        public void Close(Type plugin)
        {
            if (!TryGetPlugin(plugin, out _))
                return;

            //m_FrameNavigationService.PluginClose(plugin);
        }

        public void InitializeSystem()
        {
            //m_FrameNavigationService = ServiceBasic.GetService<IFrameNavigationService>();
            //m_ApplicationDirectory = CommonValues.GetAppDirectory(callingHost);
            LoadPlugins();
        }

        public void OpenMainPage(Type plugin)
        {
            //if (!TryGetPlugin(plugin, out IPlugabble plugabble))
            //    return;

            //m_FrameNavigationService.NavigateTo(plugin, plugabble.StartPageKey);
        }

        public void RegisterButton(IButton button)
        {
            RegisteredButtons.Add(button);
        }

        public void RegisterMenuButton(IButton button)
        {
            RegisteredMenuButtons.Add(button);
        }

        //public void RegisterSettings(IPlugabble plugin, Type settingsInterface, Type settingsImplementation)
        //{
        //    if (!typeof(ISettingsAdapter).IsAssignableFrom(settingsInterface))
        //        throw new ArgumentOutOfRangeException(nameof(settingsInterface), "does not implement ISettingsAdapter");
        //    ServiceBasic.Register(settingsInterface, settingsImplementation);
        //    var service = (ISettingsAdapter)ServiceBasic.GetService(settingsInterface);
        //    service.Initialize(Path.Combine(m_ApplicationDirectory, $"plugin_{plugin.GetType().Name}_settings.xml"));
        //}

        public void OnHostLoaded()
        {
            foreach (var plugin in m_RegisteredPlugins.Values)
            {
                plugin.OnHostLoaded();
            }
        }

        private bool TryGetPlugin(Type plugin, out IPlugabble plugabble)
        {
            if (!m_RegisteredPlugins.TryGetValue(plugin, out plugabble))
            {
                //Messages.LogEntry.Send(Messages.LogLevel.Error, LangKey.PluginServicePluginNotRegistered, plugin.Name);
                return false;
            }

            //if (m_FrameNavigationService == null)
            //{
            //    Messages.LogEntry.Send(Messages.LogLevel.Error, LangKey.PluginServiceNotInitialized);
            //    return false;
            //}

            return true;
        }

        private void LoadPlugins()
        {
            var directoryInfo = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins"));
            if (!directoryInfo.Exists)
            {
                //Messages.LogEntry.Send(Messages.LogLevel.Error, LangKey.PluginServicePluginDirectoryMissing, pluginDirectory);
                return;
            }

            foreach (var DirectoryInfo in directoryInfo.GetDirectories())
            {
                var plugabble = LoadPlugin(DirectoryInfo);
                if (plugabble == null)
                    continue;

                m_RegisteredPlugins.Add(plugabble.GetType(), plugabble);
                plugabble.OnRegister();
            }
        }

        private static IPlugabble LoadPlugin(DirectoryInfo directoryInfo)
        {
            var AssemblyFile = directoryInfo.GetFiles($"{directoryInfo.Name}.dll", SearchOption.TopDirectoryOnly).FirstOrDefault();

            var assembly = LoadAssemblyFromFileStream(AssemblyFile);
            if (assembly == null)
                return null;

            var pluginAssemblyAttribute = assembly.GetCustomAttribute<PluginAssemblyAttribute>();
            if (pluginAssemblyAttribute == null)
                return null;

            object plugin;
            //try
            //{
                plugin = Activator.CreateInstance(pluginAssemblyAttribute.EntryPoint);
            //}
            //catch (Exception ex)
            //{
            //    LoggerMessage.Send(new LogEntry(LogEntryType.Error, ex.ToString()));
            //    return null;
            //}

            if (!(plugin is IPlugabble plugabble))
            {
                //Messages.LogEntry.Send(Messages.LogLevel.Error, LangKey.PluginServiceErrorActivatingPlugin, fileInfo.Name);
                return null;
            }

            if (!plugabble.CanLoad)
                return null;

            return plugabble;
        }

        private static Assembly LoadAssemblyFromFileStream(FileInfo fileInfo)
        {
            //try
            //{
                return Assembly.Load(File.ReadAllBytes(fileInfo.FullName));
            //}
            //catch (Exception ex)
            //{
            //    LoggerMessage.Send(new LogEntry(LogEntryType.Error, ex.ToString()));
            //    return null;
            //}
        }
    }
}
