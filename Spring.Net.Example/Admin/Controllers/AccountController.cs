using Infrastructure;
using Spring.Net.Example.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spring.Net.Example.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private RSACryption rsa = new RSACryption();


        [AllowAnonymous]
        public ActionResult Login()
        {
            var model = new Models.Account.LoginOut()
            {
                CookieStartWith = Infrastructure.ConfigHelper.Instance.Get("Fm:CookieStartWith"),
                //IsMultiRegionModel = Core.SysConfigHelper.Instance.GetBool("IsMultiRegionModel"),
                IsIntensionPass = Core.SysConfigHelper.Instance.GetBool("IsIntensionPass"),
                RsaPubKeyExponent = rsa.GetPublicKey().Exponent,
                RsaPubKeyModulus = rsa.GetPublicKey().Modulus
            };

            return View("~/Admin/Views/Account/Login.cshtml", model);
        }
    }
}