using Ringen.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Core
{
    public interface IExplorerItem
    {
        string Value { get; }
        IExplorerItem ExplorerParent { get; }
    }
}
