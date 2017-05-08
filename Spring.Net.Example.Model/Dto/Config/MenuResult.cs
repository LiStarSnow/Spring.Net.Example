

namespace Cis.Model.Dto.Config
{
    /// <summary>
    /// UI配置信息
    /// </summary>
    public class MenuResult
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 父节点编号
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// 标记值
        /// </summary>
        public string Value { get; set; }
    }
}
