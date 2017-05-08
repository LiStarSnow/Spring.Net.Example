namespace Spring.Net.Example.Model.Dto.Sys.Common
{
    /// <summary>
    /// 角色信息
    /// </summary>
    public class RoleResult
    {
        /// <summary>
        /// 主键
        /// </summary>
        public decimal Id
        {
            get; set;
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get; set;
        }

        /// <summary>
        /// 状态（1：正常0：禁用）
        /// </summary>
        public string State { get; set; }


        /// <summary>
        /// 角色所属系统编码
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 角色所属系统名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 已经分配了当前角色
        /// 当用户关联查询所有角色时候，需要标记那部分已经被分配的角色
        /// </summary>
        public bool IsChecked { get; set; }
    }
}
