namespace Ringen.Core.UI
{
    public abstract class RingenTabItem : ExtendedNotifyPropertyChangedUserControl, IRingenTabItem
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        private object m_Container;
        public object Container
        {
            get { return m_Container; }
            set
            {
                m_Container = value;
            }
        }

        private string m_RingenTabItemHeaderName;
        public string RingenTabItemHeaderName
        {
            get { return m_RingenTabItemHeaderName; }
            set
            {
                m_RingenTabItemHeaderName = value;
            }
        }
    }
}
