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

    public class UserRoleDao :  IUserRoleDao
    {
        public bool Delete(string userId, List<string> appKey)
        {
            throw new NotImplementedException();
        }

        public List<UserDto> GetRoleUsers(string roleId)
        {
            throw new NotImplementedException();
        }

        public List<RoleResult> GetUserRoles(string userId, string userType, List<string> appKey)
        {
            throw new NotImplementedException();
        }

        public bool Insert(string userId, List<string> roleIds)
        {
            throw new NotImplementedException();
        }

        public void RoleAllot(string userId, List<string> roleIds, List<string> appKey)
        {
            throw new NotImplementedException();
        }

        public bool ValidRoleHasUser(string roleId)
        {
            throw new NotImplementedException();
        }
    }
}
