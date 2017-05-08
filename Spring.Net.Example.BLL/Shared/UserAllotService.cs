

namespace Spring.Net.Example.BLL.Shared
{
    using System.Linq;
    using System.Collections.Generic;
    using IDao;
    using Model.Dto.Shared;
    using IDao.Sys;
    using IBLL.Shared;

    /// <summary>
    /// 用户查询权限验证帮助服务
    /// </summary>
    public class UserAllotService : IUserAllotService
    {
        private IRoleMenuDao RoleMenuDao { get; set; }
        private IUserDao UserDao { get; set; }

        /// <summary>
        /// 验证用户是否拥有该菜单的访问权限
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="validCode">验证码</param>
        /// <returns></returns>
        public bool ValidateMenuAllot(string userId, string[] validCode)
        {
            return RoleMenuDao.ValidateMenuAllot(Global.CurrentApp.AppKey, userId, validCode.ToList());
        }

        /// <summary>
        /// 验证用户是否拥有该菜单的访问权限
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="validCode">验证码</param>
        /// <returns></returns>
        public bool ValidateUserLoginKey(string userId, string loginKey)
        {
            return UserDao.ValidateUserLoginKey(userId, loginKey);
        }


        /// <summary>
        /// 根据用户权限信息与当前用户选择权限信息获取当前查询权限范围
        /// </summary>
        /// <param name="allotVals">用户权限信息</param>
        /// <param name="values">选择的权限</param>
        /// <param name="allotName">权限名称</param>
        /// <returns></returns>
        public static List<string> GetCheckAllots(IEnumerable<TextValue> allotVals, IEnumerable<string> values, string allotName)
        {
            var userAllots = allotVals.Where(x => x.IsAlloted);
            if (userAllots.Count() == 0)
            {
                throw new System.Exception("缺少" + allotName + "权限，请联系管理员！");
            }
            var chkAllots = values == null || values.Count() == 0 ? userAllots : userAllots.Where(x => values.Contains(x.Value));
            if (chkAllots.Count() == 0)
            {
                throw new System.Exception("查询参数" + allotName + "输入参数无效！");
            }
            if (allotVals.Count() == chkAllots.Count())
            {
                return null;
            }
            return chkAllots.Select(x => x.Value.ToString()).ToList();
        }
    }
}
