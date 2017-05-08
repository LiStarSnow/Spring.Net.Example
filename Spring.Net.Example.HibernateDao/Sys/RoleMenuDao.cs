using System;
using System.Collections.Generic;
using NHibernate;
namespace Spring.Net.Example.HibernateDao.Sys
{
    using IDao.Sys;
    using Model.Dto.Sys.Role.Role;

    public class RoleMenuDao :  IRoleMenuDao
    {
        public List<RoleMenuResult> GetRoleFuncs(string id)
        {
            throw new NotImplementedException();
        }

        public List<RoleMenuResult> GetRoleMenus(string id)
        {
            throw new NotImplementedException();
        }

        public bool ValidateMenuAllot(List<string> appkey, string userId, List<string> code)
        {
            throw new NotImplementedException();
        }
    }
}
