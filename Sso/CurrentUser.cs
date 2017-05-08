using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sso
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class CurrentUser
    {
        /// <summary>
        /// 主键,标识列,序列 就这样
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
        /// 机构编码
        /// </summary>
        public string OrgId { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }

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
        /// 统筹区名称
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 针对审核系统的 多数据库模式
        /// </summary>
        public string DbKey { get; set; }

        /// <summary>
        /// 针对审核系统的 多统筹区模式
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 附加用户信息 用于子系统用户权限
        /// </summary>
        public string Append { get; set; }

        /// <summary>
        /// 用户工号
        /// </summary>
        public string WorkNo { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// 用户登录码 验证用户是否已重新登录
        /// </summary>
        public string LoginKey { get; set; }
    }
}
