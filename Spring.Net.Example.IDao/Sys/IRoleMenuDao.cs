using Spring.Net.Example.Model.Dto.Sys.Role.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.IDao.Sys
{
    public interface IRoleMenuDao
    {
        /// <summary>
        /// 获取角色菜单功能权限
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns>返回角色菜单功能权限信息</returns>
        List<RoleMenuResult> GetRoleMenus(string id);
        /// <summary>
        /// 获取角色功能权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<RoleMenuResult> GetRoleFuncs(string id);

        /// <summary>
        /// 判断用户验证码是否属于权限内
        /// </summary>
        /// <param name="userId">用户编码</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        bool ValidateMenuAllot(List<string> appkey, string userId, List<string> code);
    }
}
