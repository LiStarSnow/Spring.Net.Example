using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.EFDao
{
    public interface IConfiguration
    {
        void Regist(ConfigurationRegistrar configurations);
    }
}
