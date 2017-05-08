/* ***********************************************
 * author :  何泽立
 * function: 人员类型
 * history:  created by 何泽立 2015/10/08
 * ***********************************************/

namespace Cis.Fm.Model.Dto.Sys.User
{
    /// <summary>
    /// 人员类型信息
    /// </summary>
    public class UserBenefitResult
    {
        /// <summary>
        /// 人员类型id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 人员类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父节点id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 是否有权限
        /// </summary>
        public bool IsAlloted { get; set; }
    }
}
