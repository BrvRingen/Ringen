using System.Windows.Input;

namespace Ringen.Core.UI
{
    public interface IButton
    {
        ICommand Command { get; }
        object Icon { get; }
        object Content { get; }
    }
}
