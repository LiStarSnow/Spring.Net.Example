using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.Model.Table
{
    public class FM_ROLE
    {
        public virtual int ID { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public virtual string ROLE_NAME { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string REMARK { get; set; }
        /// <summary>
        /// 状态（1：正常0：禁用）
        /// </summary>
        public virtual string STATE { get; set; }
        /// <summary>
        /// 应用系统标识
        /// </summary>
        public virtual string SYS_APP_KEY { get; set; }

        /// <summary>
        /// 角色下的用户列表
        /// </summary>
        public virtual ISet<FM_USER> Users { get; set; }

        /// <summary>
        /// 角色拥有的菜单
        /// </summary>
        public virtual ISet<FM_MENU> Menus { get; set; }
    }
}
