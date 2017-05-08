

namespace Spring.Net.Example.Model.Dto.Config
{
    /// <summary>
    /// 系统配置项信息
    /// </summary>
    public class ParamResult
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 页面
        /// </summary>
        public string Page { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 标记
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 是否可编辑
        /// </summary>
        public string IsEdit { get; set; }

        /// <summary>
        /// 组件类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        public string Verify { get; set; }

        /// <summary>
        /// 是否客户端使用
        /// </summary>
        public string IsClientUse { get; set; }

        /// <summary>
        /// 值类型(1:字符串;2:数字;3:布尔型;4:浮点型;)
        /// </summary>
        public string ValueType { get; set; }

        /// <summary>
        /// 系统标识
        /// </summary>
        public string AppKey { get; set; }
    }
}
