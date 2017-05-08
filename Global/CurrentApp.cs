using System.Collections.Generic;

namespace Global
{

    public class TempUser
    {
        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; }
    }
    /// <summary>
    /// 当前请求信息
    /// </summary>
    public static class CurrentApp
    {
        public static string LoginCookieName { get; set; } = "";
        public static string WebRootPath { get; set; } = "";
        public static string FilesPath { get; set; } = "";
        public static string LogsPath { get; set; } = "";

        private static List<string> appKey = null;
        public static List<string> AppKey
        {
            get
            {
                if (appKey == null)
                {
                    appKey = new List<string>(Infrastructure.ConfigHelper.Instance.Get("Fm:AppKey").Split(','));
                }
                return appKey;
            }
        }
    }
}