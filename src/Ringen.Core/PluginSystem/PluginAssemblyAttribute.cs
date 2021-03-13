using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
