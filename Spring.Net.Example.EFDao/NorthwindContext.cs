using EFTracingProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.EFDao
{
    public class NorthwindContext : EFContext
    {

        public NorthwindContext(string connectionName)
            : base(EFTracingProviderUtils.CreateTracedEntityConnection(connectionName))
        {
            this.EnableTracing();
        }
    }
}
}
