using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Spring.Net.Example.Server
{
    /// <summary>
    /// 自定义json序列化  解决日期格式问题
    /// </summary>
    public class BaseJsonResult : JsonResult
    {
        public BaseJsonResult(object data)
        {
            base.Data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Write(JsonConvert.SerializeObject(base.Data));
        }
    }

    public class BaseController : Controller
    {
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new BaseJsonResult(data);
        }
    }
}
