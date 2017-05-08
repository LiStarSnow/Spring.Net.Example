using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace Spring.Net.Example.HibernateDao.Sys
{
    using IDao.Sys;
    using Model.Dto.Sys.Common;
    public class RoleDao : IRoleDao
    {
        public bool ExistRoleName(string id, string name, string appKey)
        {
            throw new NotImplementedException();
        }

        public string GetRoleIdByName(string name, string appKey)
        {
            throw new NotImplementedException();
        }

        public List<RoleResult> GetRoles(List<string> appKey, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
