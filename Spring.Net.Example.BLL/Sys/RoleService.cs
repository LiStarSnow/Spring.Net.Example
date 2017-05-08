
using Spring.Net.Example.IBLL.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Net.Example.IDao.Sys;
using Spring.Net.Example.Model.Dto.Sys.Common;

namespace Spring.Net.Example.BLL.Sys
{
    public class RoleService : IRoleService
    {
        public IRoleDao RoleDao { get; set; }

        public bool ExistRoleName(string id, string name, string appKey)
        {
             return RoleDao.ExistRoleName(id, name, appKey);
        }

        public string GetRoleIdByName(string name, string appKey)
        {
            return RoleDao.GetRoleIdByName(name, appKey);
        }

        public List<RoleResult> GetRoles(List<string> appKey, string userId)
        {
            return RoleDao.GetRoles(appKey, userId);
        }
    }
}
