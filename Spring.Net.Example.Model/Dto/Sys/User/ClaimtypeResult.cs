/* ***********************************************
 * author :  何泽立
 * function: 就医方式
 * history:  updated by 何泽立 2015/08/05
 * ***********************************************/

namespace Cis.Fm.Model.Dto.Sys.User
{
    /// <summary>
    /// 就医方式
    /// </summary>
    public class ClaimtypeResult
    {
        /// <summary>
        /// 就医方式Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 就医方式名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 当前是否被分配了该记录
        /// </summary>
        public bool IsChecked { get; set; } 
    }
}