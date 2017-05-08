/* ***********************************************
 * author :  何泽立
 * function: 参保类型
 * history:  updated by 何泽立 2015/08/05
 * ***********************************************/

namespace Cis.Fm.Model.Dto.Sys.User
{
    /// <summary>
    /// 参保类型
    /// </summary>
    public class BenefitplanResult
    {
        /// <summary>
        /// 参保类型ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 参保类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 当前是否被分配了该记录
        /// </summary>
        public bool IsChecked { get; set; }
    }
}