using Cis.DbLight.TableMetadata;

namespace Cis.Fm.Model.Dto.Sys.Application
{
    public class FmApplicationResult
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 程序key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 系统地址
        /// </summary>
        public string AppUri { get; set; }

        /// <summary>
        /// 系统图标
        /// </summary>
        public string IconUri { get; set; }

        /// <summary>
        /// 系统图标
        /// </summary>
        public string IconUriFocus { get; set; }

        /// <summary>
        /// 登录地址
        /// </summary>
        public string LoginUri { get; set; }

        /// <summary>
        /// 退出登录地址
        /// </summary>
        public string LoginOutUri { get; set; }

        /// <summary>
        /// 打开方式
        /// </summary>
        public string OpenModel { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked { get { return !string.IsNullOrEmpty(AppUri); } }

        /// <summary>
        /// 系统状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Prompt { get; set; }

        /// <summary>
        /// 系统类型
        /// </summary>
        public string ApplicationType { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public decimal Sort { get; set; }

        /// <summary>
        /// 令牌
        /// </summary>
        public string Token { get; set; }
    }
}
