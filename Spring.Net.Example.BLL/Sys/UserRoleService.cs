using Spring.Net.Example.IBLL.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Net.Example.Model.Dto.Sys.Common;
//using Spring.Transaction.Support;
using Spring.Net.Example.IDao.Sys;

namespace Spring.Net.Example.BLL.Sys
{
    public class UserRoleService : IUserRoleService
    {
        //public Transaction.IPlatformTransactionManager TransactionManager { get; set; }

        public IUserRoleDao UserRoleDao { get; set; }

        public List<UserDto> GetRoleUsers(string roleId)
        {
            throw new NotImplementedException();
        }

        public List<RoleResult> GetUserRoles(string userId, string userType, List<string> appKey)
        {
            throw new NotImplementedException();
        }

        public void RoleAllot(string userId, List<string> roleIds, List<string> appKey)
        {
            //TransactionTemplate tt = new TransactionTemplate(TransactionManager);

        }

        public bool ValidRoleHasUser(string roleId)
        {
            throw new NotImplementedException();
        }
    }
}
