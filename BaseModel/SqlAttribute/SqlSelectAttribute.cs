using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseModel.SqlAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SqlSelectAttribute : SqlAttribute
    {

        /// <summary>
        /// 自定义:{0}名称{1}参数名
        /// </summary>
        public string Format { get; set; }

        //public SqlSelectAttribute(string colName)
        //{
        //    this.ColName = colName;
        //}
    }
}
