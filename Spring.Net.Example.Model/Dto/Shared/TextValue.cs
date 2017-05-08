using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spring.Net.Example.Model.Dto.Shared
{
    /// <summary>
    /// 字典数据
    /// </summary>
    public class TextValue
    {
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 是否分配权限
        /// </summary>
        public bool IsAlloted { get; set; } = true;

        public int Code { get; set; } = 0;
    }
}
