using Global;
using Log;
using Newtonsoft.Json;
using Spring.Net.Example.Model.Dto.Shared;
using Spring.Net.Example.Server.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Spring.Net.Example.Server.AppStart
{
    public class RegisterFrameWork
    {
        public static BLL.Sys.SysService sysService { get; set; }

        public static void Register(AppStartParams param)
        {
            //RouteTable.Routes.MapRoute(
            //    name: "fm",
            //    url: "fm/{controller}/{action}",
            //    namespaces: new string[] { "Cis.Fm.Server.Controllers" }
            //);
            //RouteTable.Routes.MapRoute(
            //    name: "fmsys",
            //    url: "fmsys/{controller}/{action}",
            //    namespaces: new string[] { "Cis.Fm.Server.Controllers.Sys" }
            //);
            //RouteTable.Routes.MapRoute(
            //    name: "fmconfig",
            //    url: "fmconfig/{controller}/{action}",
            //    namespaces: new string[] { "Cis.Fm.Server.Controllers.Config" }
            //);
            //RouteTable.Routes.MapRoute(
            //    name: "fmshared",
            //    url: "fmshared/{controller}/{action}",
            //    namespaces: new string[] { "Cis.Fm.Server.Controllers.Shared" }
            //);

            GlobalFilters.Filters.Add(new ActionExceptionAttribute());

            //AutoMapperInit.InitMapper(); 初始化实体映射

            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
            {
                return setting;
            });

            //CurrentApp.LoginCookieName = System.Web.Security.FormsAuthentication.FormsCookieName;
            CurrentApp.WebRootPath = param.RootPath + "\\";
            CurrentApp.FilesPath = CurrentApp.WebRootPath + "files\\";
            CurrentApp.LogsPath = CurrentApp.WebRootPath + "logs\\";

            //配置Nlog
            Logger.LoggergConfiguration(CurrentApp.LogsPath);

            ////加载系统配置信息
            sysService.RefreshConfigValue();

            //加载配置后，判断目前的日志是否开启全量模式
            if (Core.SysConfigHelper.Instance.GetBool("SysLog"))
            {
                Logger.UpdateLoggergConfiguration(CurrentApp.LogsPath, true);
            }

        }

    }
}
