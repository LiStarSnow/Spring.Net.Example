using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.Model.Table
{
    public class FM_MENU
    {
        /// <summary>
        /// 菜单编号，主键列
        /// </summary>
        public virtual int ID { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public virtual string MENU_NAME { get; set; }
        /// <summary>
        /// 菜单类型(1:业务菜单      ;2:系统菜单;3:公共菜单;4:管理员菜单)
        /// </summary>
        public virtual string MENU_TYPE { get; set; }

        /// <summary>
        /// 操作类型（1:目录 2:菜单 3:功能）
        /// </summary>
        public virtual string OPERATION_TYPE { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public virtual int SORT { get; set; }
        /// <summary>
        /// 父节点编号
        /// </summary>
        public virtual int PARENT_ID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string REMARK { get; set; }
        /// <summary>
        /// 状态(0:启用      ;1:停用)
        /// </summary>
        public virtual string STATE { get; set; }

        /// <summary>
        /// 视图参数
        /// </summary>
        public virtual string VIEW_PARAMS { get; set; }
        /// <summary>
        /// 验证编码
        /// </summary>
        public virtual string VALIDATE_CODE { get; set; }
        /// <summary>
        /// 菜单打开视图调用方法
        /// </summary>
        public virtual string HANDLER { get; set; }
        /// <summary>
        /// 菜单对应视图
        /// </summary>
        public virtual string MENU_VIEW { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        public virtual string ICON { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string IS_SYS_MENU { get; set; }
        /// <summary>
        /// 系统标识
        /// </summary>
        public virtual string SYS_APP_KEY { get; set; }

        public virtual ISet<FM_ROLE> Roles { get; set; }
    }
}
