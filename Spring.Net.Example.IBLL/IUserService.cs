using BaseDto;
using Spring.Net.Example.Model;
using Spring.Net.Example.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.IBLL
{
    public interface IUserService : IService<FM_USER>
    {

        System.Collections.IList Test();
        /// <summary>
        /// 获取 所有用户信息列表
        /// </summary>
        /// <returns></returns>
        IList<FM_USER> GetAllUsers();

        /// <summary>
        /// 根据id来获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        FM_USER GetUserById(int userId);

        Response<bool> RoleAllot(string userId, List<string> roleIds);

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
