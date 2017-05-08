using Spring.Net.Example.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Spring.Net.Example.Server.Filters
{
    public class ActionExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// The on exception.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public void OnException(ExceptionContext context)
        {
            var result = new Response<bool>();

            //if (context.Exception is DataAccessExcepton)
            //{
            //    var dataAccessExcepton = context.Exception as DataAccessExcepton;

            //    if (dataAccessExcepton != null)
            //    {
            //        result.ErrMsg = dataAccessExcepton.Message;
            //    }
            //}
            //else
            //{
            result.ErrMsg = "系统发生错误，请联系管理人员进行反馈。";
            //Logger.LogException(context.Exception);
            //}

            //异常已处理，不需要后续操作
            context.ExceptionHandled = true;

            context.Result = new JsonResult() { Data = result };
        }
    }
}
