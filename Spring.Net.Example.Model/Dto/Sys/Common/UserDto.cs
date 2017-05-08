
using System;
using System.Collections.Generic;

namespace Spring.Net.Example.Model.Dto.Sys.Common
{
    /// <summary>
    /// 服务端用户缓存信息
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// 是否内置角色 1：是0：否
        /// </summary>
        public string IsSysFlag { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public int Random { get; set; }

        /// <summary>
        /// 针对审核系统的 多数据库模式
        /// </summary>
        public string DbKey { get; set; }

        /// <summary>
        /// 附加用户信息 用于子系统用户权限
        /// </summary>
        public Dictionary<string, string> Append { get; set; }
    }
}
