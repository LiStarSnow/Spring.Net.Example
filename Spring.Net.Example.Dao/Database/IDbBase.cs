using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Spring.Net.Example.Dao.Database
{
    public interface IDbBase
    {
        DbCommand CreateCommand();
        DbConnection CreateConnection();
        DbDataAdapter CreateDataAdapter();
        DbParameter CreateParameter();
    }
}
