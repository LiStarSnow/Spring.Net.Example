using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseModel.Enum
{
    public enum ConditionType
    {
        /// <summary>
        /// string: null empty
        /// list  : null count=0
        /// datetime: null MinValue
        /// int long decimal Enum : 0
        /// </summary>
        IsNullOrEmpty = 0,
        /// <summary>
        /// 
        /// </summary>
        NotEqualValue = 1
    }
}
