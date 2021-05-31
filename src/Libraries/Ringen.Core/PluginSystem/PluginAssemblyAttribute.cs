using System;

namespace Ringen.Core.PluginSystem
{
    public sealed class PluginAssemblyAttribute : Attribute
    {
        public Type EntryPoint { get; }

        public PluginAssemblyAttribute(Type entryPoint)
        {
            EntryPoint = entryPoint;
        }
    }
}
