/* ***********************************************
 * author :  何泽立
 * function: 个人信息修改 用户信息
 * history:  created by 何泽立 2015/07/20
 * ***********************************************/

namespace Cis.Fm.Model.Dto.Sys.User
{
    /// <summary>
    /// 个人信息修改 用户信息
    /// </summary>
    public class UserModifyResult : UserResult
    {
        /// <summary>
        /// 原密码
        /// </summary>
        public string OldPass { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPass { get; set; }

        /// <summary>
        /// 是否修改密码
        /// </summary>
        public bool OperPass { get; set; }
    }
}
