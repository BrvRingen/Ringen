using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Core.UI
{
    public interface IRingenTabItem : IDisposable
    {
        object Container { get; set; }
        string RingenTabItemHeaderName { get; set; }
    }
}
