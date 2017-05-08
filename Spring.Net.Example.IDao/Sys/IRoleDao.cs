using Spring.Net.Example.Model.Dto.Sys.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.IDao.Sys
{
    public interface IRoleDao
    {
        List<RoleResult> GetRoles(List<string> appKey, string userId);

        string GetRoleIdByName(string name, string appKey);

        bool ExistRoleName(string id, string name, string appKey);
    }
}
