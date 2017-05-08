using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;
using Cis.Global;
using Cis.Service.Shared;
using Cis.BaseDto;
using Cis.Infrastructure;
using Cis.Log;
using System.Text;

namespace Cis.Server.Filters
{
    public class VisitedAttribute : ActionFilterAttribute
    {
        public VisitedAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            try
            {
                if (Core.SysConfigHelper.Instance.GetBool("SysLog"))
                {
                    var sb = new StringBuilder();
                    sb.AppendLine(" URL:" + request.Url.AbsoluteUri);
                    sb.AppendLine(" UrlReferrer:" + request.UrlReferrer);

                    if (request.QueryString.Count > 0)
                    {
                        sb.AppendLine(" QueryString:");
                        foreach (var item in request.QueryString.AllKeys)
                        {
                            sb.AppendLine(item + ":" + request.QueryString[item]);
                        }
                    }
                    if (request.Form.Count > 0)
                    {
                        sb.AppendLine(" Form:");
                        foreach (var item in request.Form.AllKeys)
                        {
                            sb.AppendLine(item + ":" + request.Form[item]);
                        }
                    }
                    if (request.Cookies.Count > 0)
                    {
                        sb.AppendLine(" Cookies:");
                        foreach (var item in request.Cookies.AllKeys)
                        {
                            sb.AppendLine(item + ":" + request.Cookies.Get(item).Value);
                        }
                    }
                    Log.Logger.Log(Level.Debug, sb.ToString());
                }
            }
            catch (System.Exception)
            {
            }

            base.OnActionExecuting(filterContext);
        }
    }
}