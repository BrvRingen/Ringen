using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
