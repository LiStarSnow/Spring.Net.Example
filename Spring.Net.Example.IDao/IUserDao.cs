using Spring.Net.Example.Model;
using Spring.Net.Example.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.IDao
{
    public interface IUserDao : IDAO<FM_USER>
    {
        System.Collections.IList Test();
        /// <summary>
        /// 获取所有的用户
        /// </summary>
        /// <returns></returns>
        IList<FM_USER> GetAllUsers();

        ///// <summary>
        ///// 根据id来获取用户
        ///// </summary>
        ///// <param name="Id"></param>
        ///// <returns></returns>
        //FM_USER GetUserById(string Id);

        ///// <summary>
        ///// 添加用户信息
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //bool AddUser(FM_USER user);

        ///// <summary>
        ///// 更新用户信息
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //bool UpdateUser(FM_USER user);

        ///// <summary>
        ///// 更新或者添加用户信息
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //bool AddOrUpdateUser(FM_USER user);

        ///// <summary>
        ///// 删除用户
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //bool DeleteUser(FM_USER user);

        /// <summary>
        /// 验证是否为同一用户登录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="loginKey"></param>
        /// <returns></returns>
        bool ValidateUserLoginKey(string userId, string loginKey);

        /// <summary>  
        /// 分页获取数据列表  
        /// </summary>  
        /// <param name="PageSize">每页获取数据条数</param>  
        /// <param name="PageIndex">当前页是第几页</param>  
        /// <param name="strWhere">查询条件</param>  
        /// <returns></returns>  
        IList<FM_USER> GetPageList(int PageSize, int PageIndex, string field, string order, string sqlWhere);
    }
}
