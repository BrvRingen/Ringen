using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Core.PluginSystem
{
    public sealed class PluginAssemblyAttribute : Attribute
    {
        #region properties
        public Type EntryPoint { get; }
        #endregion properties

        #region constructors
        public PluginAssemblyAttribute(Type entryPoint)
        {
            EntryPoint = entryPoint;
        }
        #endregion constructors
    }
}
