using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Core.PluginSystem
{
    public interface IPlugabble
    {
        string Name { get; }
        string StartPageKey { get; }
        bool CanLoad { get; }

        void OnRegister();
        void OnHostLoaded();
    }
}
