using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Shared.ConfigurationElemente
{
    public sealed class ValueIntConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("value")]
        public int Value
        {
            get { return (int)this["value"]; }
        }
    }
}
