using System;
using System.Reflection;

namespace Ringen.Core.PluginSystem
{
    public abstract class PluggableBase : IPlugabble
    {
        public abstract string Name { get; }

        public abstract string StartPageKey { get; }

        public virtual bool CanLoad => true;

        public virtual void OnHostLoaded() { }

        public virtual void OnRegister() { }

        protected static Uri GetPluginUri(string uriPart, bool isResourceFile = true)
        {
            var callingAssembly = Assembly.GetCallingAssembly();
            return new Uri($"pack://application:,,,/{callingAssembly.GetName().Name};{(isResourceFile ? "component" : "")}/{uriPart}", UriKind.Absolute);
        }
    }
}
