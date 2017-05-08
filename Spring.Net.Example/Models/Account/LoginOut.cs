using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spring.Net.Example.Models.Account
{
    public class LoginOut
    {
        public bool IsIntensionPass;

        public string LoginUrl { get; set; }

        public bool IsMultiRegionModel { get; set; } = false;

        public string RsaPubKeyExponent { get; set; }

        public string CookieStartWith { get; set; }

        public string RsaPubKeyModulus { get; set; }

    }
}