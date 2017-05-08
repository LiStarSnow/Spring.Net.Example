

namespace Spring.Net.Example.Model.Dto.Sys.Role.Role
{
    /// <summary>
    /// 角色菜单信息
    /// </summary>
    public class RoleMenuResult
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName
        {
            get; set;
        }

        /// <summary>
        /// 操作类型 0:目录1:菜单2:功能
        /// </summary>
        public string OperationType { get; set; }

        public string Remark { get; set; }

        /// <summary>
        /// 父菜单编号
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public decimal Sort
        {
            get; set;
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked
        {
            get; set;
        }
    }
}
