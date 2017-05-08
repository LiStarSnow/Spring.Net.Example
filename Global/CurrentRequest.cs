//using Cis.BaseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Security;

namespace Global
{
    /// <summary>
    /// 当前请求信息
    /// </summary>
    public class CurrentRequest
    {
        public static bool IsHttp
        {
            get
            {
                try
                {
                    var s = HttpContext.Current.Request;
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        //public static Dictionary<string, object> UserCookies
        //{
        //    get
        //    {
        //        var userCookie = HttpContext.Current.Request.Cookies.Get(CurrentApp.LoginCookieName);
        //        if (userCookie == null)
        //        {
        //            return new Dictionary<string, object>();
        //        }
        //        try
        //        {
        //            return JsonConvert.DeserializeObject<Dictionary<string, object>>(
        //            FormsAuthentication.Decrypt(userCookie.Value).UserData);
        //        }
        //        catch
        //        {
        //            return new Dictionary<string, object>();
        //        }
        //    }
        //}

        /// <summary>
        /// 获取Cookie对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetLoginInfo<T>() where T : class
        {
            var userCookie = HttpContext.Current.Request.Cookies.Get(CurrentApp.LoginCookieName);
            if (userCookie != null)
            {
                string data = FormsAuthentication.Decrypt(userCookie.Value).UserData;
                return JsonConvert.DeserializeObject<T>(data);
            }
            return null;
        }

        /// <summary>
        /// 获取请求IP
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string IP
        {
            get
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
        }

        ///// <summary>
        ///// 获取用户名称
        ///// </summary>
        ///// <param name="context"></param>
        ///// <returns></returns>
        //public static string Name
        //{
        //    get
        //    {
        //        return UserCookies.ContainsKey("Name") ? UserCookies["Name"].ToString() : null;
        //    }
        //}

        ///// <summary>
        ///// 获取用户类型 1:审核用户 2:机构用户
        ///// </summary>
        ///// <param name="context"></param>
        ///// <returns></returns>
        //public static string UserType
        //{
        //    get
        //    {
        //        return UserCookies.ContainsKey("UserType") ? UserCookies["UserType"].ToString() : null;
        //    }
        //}

        ///// <summary>
        ///// 获取用户登录名
        ///// </summary>
        ///// <param name="context"></param>
        ///// <returns></returns>
        //public static string Code
        //{
        //    get
        //    {
        //        return UserCookies.ContainsKey("Code") ? UserCookies["Code"].ToString() : null;
        //    }
        //}

        ///// <summary>
        ///// 获取用户Id
        ///// </summary>
        ///// <param name="context"></param>
        ///// <returns></returns>
        //public static string UserId
        //{
        //    get
        //    {
        //        return UserCookies.ContainsKey("Id") ? UserCookies["Id"].ToString() : null;
        //    }
        //}

        ///// <summary>
        ///// 获取用户机构Id
        ///// </summary>
        ///// <param name="context"></param>
        ///// <returns></returns>
        //public static string OrgId
        //{
        //    get
        //    {
        //        return UserCookies.ContainsKey("OrgId") ? UserCookies["OrgId"].ToString() : null;
        //    }
        //}

        ///// <summary>
        ///// 获取用户当前登录的统筹区ID
        ///// </summary>
        ///// <param name="context"></param>
        ///// <returns></returns>
        //public static string UserRegion
        //{
        //    get
        //    {
        //        return UserCookies.ContainsKey("BmiCode") ? UserCookies["BmiCode"].ToString() : null;
        //    }
        //}

        ///// <summary>
        ///// 统筹区名称
        ///// </summary>
        //public static string UserRegionName
        //{
        //    get
        //    {
        //        return UserCookies.ContainsKey("BmiName") ? UserCookies["BmiName"].ToString() : null;
        //    }
        //}

        /// <summary>
        /// 获取用户当前登录的统筹区ID
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string DbKey
        {
            get
            {
                var userCookie = HttpContext.Current.Request.Cookies.Get(CurrentApp.LoginCookieName);
                if (userCookie != null)
                {
                    string data = FormsAuthentication.Decrypt(userCookie.Value).UserData;
                    var dicData = JsonConvert.DeserializeObject<Dictionary<string, object>>(FormsAuthentication.Decrypt(userCookie.Value).UserData);
                    return dicData["DbKey"].ToString();
                }
                return null;
            }
        }

        ///// <summary>
        ///// 根据cookie名称获取值
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public static string GetValue(string key)
        //{
        //    return UserCookies.ContainsKey(key) ? UserCookies[key].ToString() : null;
        //}
    }
}