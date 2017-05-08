/* ***********************************************
 * author :  何泽立
 * function: 定点机构分组详细信息列表数据
 * history:  created by 何泽立 2015/07/03
 * ***********************************************/

namespace Cis.Fm.Model.Dto.Sys.Common
{
    /// <summary>
    /// 分组详细机构信息
    /// </summary>
    public class UserHospitalResult
    {
        /// <summary>
        /// 机构编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public string HospitalLevel { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string HospitalType { get; set; }

        /// <summary>
        /// 所属中心
        /// </summary>
        public string Center { get; set; }

        /// <summary>
        /// 是否拥有权限
        /// </summary>
        public bool IsAlloted { get; set; }
    }
}
