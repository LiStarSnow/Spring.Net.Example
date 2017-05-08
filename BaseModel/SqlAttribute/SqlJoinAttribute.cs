using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseModel.SqlAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SqlJoinAttribute : Attribute
    {
        /// <summary>
        /// {0} 占位符为 参数  value
        /// </summary>
        public string JoinString { get; set; }
    }
}
