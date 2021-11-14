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
