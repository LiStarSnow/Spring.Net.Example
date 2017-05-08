using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseModel.SqlAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SqlAttribute : Attribute
    {
        /// <summary>
        /// 表别名
        /// </summary>
        public string TableAlias { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColName { get; set; }
    }
}
