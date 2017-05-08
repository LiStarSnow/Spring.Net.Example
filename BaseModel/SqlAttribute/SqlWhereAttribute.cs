using BaseModel.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseModel.SqlAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SqlWhereAttribute : SqlAttribute
    {
        private string formula = "=";

        /// <summary>
        /// 条件是否必须加 优先级大于 IsSingle
        /// </summary>
        public bool IsRequire { get; set; } = false;

        /// <summary>
        /// 是否独立条件，满足则其他条件失效
        /// </summary>
        public bool IsSingle { get; set; } = false;

        /// <summary>
        /// in notin 的字符串分隔符
        /// </summary>
        public char InSplit { get; set; } = ',';

        /// <summary>
        /// Where查询方式
        /// </summary>
        public string Formula
        {
            get
            {
                return formula;
            }
            set
            {
                var temp = "";
                if (!string.IsNullOrEmpty(value))
                {
                    temp = value.ToLower().Trim();
                }
                formula = temp;
            }
        }

        /// <summary>
        /// Formula为 in notin 时 ‘全部’ 的值
        /// </summary>
        public object Values { get; set; } = null;

        /// <summary>
        /// 拼接条件
        /// </summary>
        public ConditionType ConditionType { get; set; } = ConditionType.IsNullOrEmpty;
        
        /// <summary>
        /// 日期格式  此项不为空的时候 日期真实类型会转为字符串
        /// </summary>
        public string DateTimeFormat { get; set; }

        /// <summary>
        /// 此属性不为空时in not in 使用临时表方案
        /// </summary>
        public string TmpTableName { get; set; }

        /// <summary>
        /// 临时表列名
        /// </summary>
        public string TmpTableColName { get; set; }
    }
}
