using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.Model.Dto
{
    [Description("获取单个实体响应")]
    public class Response<T>
    {
        public Response() { }

        [Description("附加信息")]
        public object AttachedObject { get; set; }
        [Description("错误码")]
        public string ErrCode { get; set; }
        [Description("错误信息")]
        public string ErrMsg { get; set; }
        [Description("响应结果是否成功")]
        public bool IsSuccess { get; }
        [Description("子项")]
        public T Result { get; set; }
    }
}
