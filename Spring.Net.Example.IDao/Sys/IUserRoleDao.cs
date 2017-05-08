using Spring.Net.Example.Model.Dto.Sys.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.IDao.Sys
{
    public interface IUserRoleDao
    {
        /// <summary>
        /// 用户角色分配
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="roleIds">角色编号</param>
        void RoleAllot(string userId, List<string> roleIds, List<string> appKey);

        /// <summary>
        /// 获取用户对应的角色
        /// </summary>
        /// <param name="userId"> The user Id. </param>
        /// <param name="userType">用户类型</param>
        /// <returns> The <see cref="List"/>. </returns>
        List<RoleResult> GetUserRoles(string userId, string userType, List<string> appKey);

        bool Delete(string userId, List<string> appKey);

        bool Insert(string userId, List<string> roleIds);

        /// <summary>
        /// 角色下是否拥有用户
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        bool ValidRoleHasUser(string roleId);

        /// <summary>
        /// 获取角色用户信息
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        List<UserDto> GetRoleUsers(string roleId);
    }
}
