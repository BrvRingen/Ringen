using System;

namespace Ringen.Core.UI
{
    public interface IRingenTabItem : IDisposable
    {
        object Container { get; set; }
        string RingenTabItemHeaderName { get; set; }
    }
}
