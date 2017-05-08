/* ***********************************************
 * author :  汪振
 * function: 用户登录
 * history:  created by 汪振 2015/07/03
 * ***********************************************/

namespace Cis.Fm.Model.Dto.Sys.User
{
    /// <summary>
    /// 用户登录
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// 统筹区
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd { get; set; }

        /// <summary>
        /// 系统类型
        /// </summary>
        public string SystemType { get; set; }
    }
}
