using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;
using Spring.Net.Example.Model.Dto;
using Global;
using Infrastructure;
//using Spring.Net.Example.BLL.Shared;
//using Cis.Global;
//using Cis.Service.Shared;
//using Cis.BaseDto;
//using Cis.Infrastructure;

namespace Spring.Net.Example.Server.Filters
{
    public class ValidateAttribute : ActionFilterAttribute
    {
        //private readonly UserAllotService userAllotService;
        public ValidateAttribute()
        {
//userAllotService = new UserAllotService();
        }
        /// <summary>
        /// 权限码 空和null 只验证登录
        /// </summary>
        public string ValidateCode { get; set; }

        private string GetUrlKey(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            string key = request.Url.AbsolutePath.ToLower();
            if (key.StartsWith(request.ApplicationPath.ToLower()))
            {
                key = key.Remove(0, request.ApplicationPath.Length);
            }
            if (key.EndsWith("/"))
            {
                key = key.Remove(key.Length - 1, 1);
            }
            if (key.Length == 0)
            {
                key = "_default";
            }
            else
            {
                if (!key.StartsWith("/"))
                {
                    key = "/" + key;
                }
            }
            return key;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            if (Core.SysConfigHelper.Instance.GetBool("IsClientIpSafe"))
            {
                var referer = request.UrlReferrer;
                var allowReferrerHost = Infrastructure.ConfigHelper.Instance.GetSection("Fm:AllowReferrerHost");
                allowReferrerHost.Add("0", request.Url.Host);

                if (referer != null)
                {
                    if (!allowReferrerHost.ContainsValue(referer.Host))
                    {
                        filterContext.Result = new JsonResult()
                        {
                            Data = new Response<bool>()
                            {
                                Result = false,
                                ErrCode = "1004",
                                ErrMsg = "无效请求"
                            },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                        return;
                    }
                }
            }
            var cHelper = ConfigHelper.Instance;
            var key = GetUrlKey(filterContext);

            string name = "";
            var appNameInfo = cHelper.GetSection("Fm:AppNameInfo");
            if (appNameInfo.ContainsKey(key))
            {
                name = appNameInfo[key];
            }

            string logo = "";
            var appIcoInfo = cHelper.GetSection("Fm:AppIcoInfo");
            if (appIcoInfo.ContainsKey(key))
            {
                logo = appIcoInfo[key];
            }

            ////登录Url
            //string LoginUrl = string.Empty;
            ////logo地址
            //string logo = string.Empty;
            ////应用程序名
            //string name = string.Empty;

            var redic = new RedirectResult(
    string.Format("{0}?url={1}&logo={2}&name={3}", ConfigHelper.Instance.Get("Fm:LoginUrl"),
    HttpUtility.UrlEncode(request.Url.AbsoluteUri),
    HttpUtility.UrlEncode(request.Url.Scheme + "://" + request.Url.Authority + request.ApplicationPath + logo),
    HttpUtility.UrlEncode(name)));

            if (!request.IsAuthenticated)
            {
                if (request.HttpMethod.ToLower() != "get")
                {
                    filterContext.Result = new JsonResult()
                    {
                        Data = new Response<bool>()
                        {
                            Result = false,
                            ErrCode = "1002",
                            ErrMsg = "登录过期,请重新登陆"
                        }
                    };
                    return;
                }
                filterContext.Result = new RedirectResult("/loginout");
                return;
            }

            var user = CurrentRequest.GetLoginInfo<Sso.CurrentUser>();
            //if (Core.SysConfigHelper.Instance.GetBool("IsSingleUserLogin") && !userAllotService.ValidateUserLoginKey(user.Id, user.LoginKey))
            //{
            //    if (request.HttpMethod.ToLower() != "get")
            //    {
            //        filterContext.Result = new JsonResult()
            //        {
            //            Data = new Response<bool>()
            //            {
            //                Result = false,
            //                ErrCode = "1002",
            //                ErrMsg = "您的用户已在其他地方登录,请重新登陆！"
            //            }
            //        };
            //        return;
            //    }
            //    filterContext.Result = redic;
            //    return;
            //}

            ////用户访问权限验证
            //if (ValidateCode != null && !userAllotService.ValidateMenuAllot(CurrentRequest.GetLoginInfo<Model.Dto.Sys.Common.UserDto>().Id, ValidateCode.Split(',')))
            //{
            //    filterContext.Result = new JsonResult()
            //    {
            //        Data = new Response<bool>()
            //        {
            //            Result = false,
            //            ErrCode = "1003",
            //            ErrMsg = "无授权,请申请获得访问权限"
            //        }
            //    };
            //    return;
            //}

            base.OnActionExecuting(filterContext);
        }
    }
}