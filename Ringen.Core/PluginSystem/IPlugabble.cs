using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Core.PluginSystem
{
    public interface IPlugabble
    {
        #region properties
        string Name { get; }
        string StartPageKey { get; }
        bool CanLoad { get; }
        #endregion properties

        #region methods
        void OnRegister();
        void OnHostLoaded();
        #endregion methods
    }
}
