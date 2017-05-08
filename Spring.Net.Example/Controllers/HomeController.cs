using Spring.Context;
using Spring.Context.Support;
using Spring.Net.Example.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Spring.Net.Example.IBLL;
using Common.Logging;


namespace Spring.Net.Example.Controllers
{
    using IBLL.Shared;
    using IBLL.Sys;
    using Model.Table;

    public class HomeController : Controller
    {
        public string Message { get; set; }
        private IUserService UserService { get; set; }
        public IUserAllotService UserAllotService { get; set; }

        private IMenuService MenuService { get; set; }

        private BLL.Sys.SysService sysService { get; set; }

        public ActionResult Index()
        {
            ILog LOGGER = LogManager.GetLogger("Test");
            LOGGER.Debug("测试信息");

            //Common.Logging.ILoggerFactoryAdapter

            //Spring.Context.Support.NamespaceParsersSectionHandler ConfigParsersSectionHandler

            //ConfigurationUtils

            //IApplicationContext ctx = ContextRegistry.GetContext();

            //IUserService userService = ctx.GetObject("userService") as IUserService;

            //IResource input = new FileSystemResource("~/Config/spring.xml");
            //IObjectFactory factory = new XmlObjectFactory(input);

            //IApplicationContext context = new XmlApplicationContext("file://~/Config/spring.xml");

            //// of course, an IApplicationContext is also an IObjectFactory...
            //IObjectFactory factory = (IObjectFactory)context;

            //Hello objHello = factory.GetObject("myHello") as Hello; // gets the object defined as 'foo'

            var userList = UserService.GetAllUsers();

            //var userlist = UserService.GetPageList(1, 2, "SORT", "desc", " and 1=1");

            //var res = UserService.Test();

            var user = UserService.GetUserById(1);

            //var res = userService.RoleAllot("admin", new List<string> { "99", "22", "33" });

            //UserAllotService.ValidateUserLoginKey("", "");

            //FM_USER user = new FM_USER()
            //{
            //    USER_CODE = "LIJINSHI",
            //    USER_NAME = "LiJinshi Test",
            //    USER_PASSWORD = "a4ayc/80/OGda4BO/1o/V0etpOqiLx1JwB5S3beHW0s=",
            //    USER_TYPE = "1",
            //    SORT = 1,
            //    ENABLE_FLAG = "1",
            //    ISSYS_FLAG = "0",
            //};
            //UserService.AddUser(user);

            ViewBag.Message = Message;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}