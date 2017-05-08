using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.HibernateDao
{
    public class SQLWatcher : EmptyInterceptor
    {
        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            
            System.Diagnostics.Debug.WriteLine("sql语句:" + sql);
            return base.OnPrepareStatement(sql);
        }
    }
}
