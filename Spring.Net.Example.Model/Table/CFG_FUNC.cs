using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.Model.Table
{
    [Serializable]
    [Table("CFG_FUNC")]
    public class CFG_FUNC
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int ID { get; set; }
        /// <summary>
        /// 功能分组名称
        /// </summary>
        public virtual string GROUP_NAME { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public virtual string NAME { get; set; }
        /// <summary>
        /// 功能备注
        /// </summary>
        public virtual string REMARK { get; set; }
        /// <summary>
        /// 功能值
        /// </summary>
        public virtual string VALUE { get; set; }
        /// <summary>
        /// 功能Key
        /// </summary>
        public virtual string KEY { get; set; }
        /// <summary>
        /// 是否可以修改（0:不可以修改  1:可以修改）
        /// </summary>
        public virtual string IS_EDIT { get; set; }
        /// <summary>
        /// 是否显示（0:不显示(未完成)  1:显示  2:不显示(仍可能使用)）
        /// </summary>
        public virtual string IS_DISPLAY { get; set; }
        /// <summary>
        /// 控件类型（1:文本框 2:单选框 3:下拉框 4:月份控件 5:数字控件）
        /// </summary>
        public virtual string CONTROL_TYPE { get; set; }
        /// <summary>
        /// 功能输入值验证{type:'integer/persent/date/',max:10,min:1,len:5}
        /// </summary>
        public virtual string VERIFY { get; set; }

        /// <summary>
        /// 界面上对应分块业务的排序（1:基础业务  2:反馈业务  3:日期业务 4:审核业务）
        /// </summary>
        public virtual string GROUP_SORT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string TAB_ID { get; set; }

        /// <summary>
        /// 客户端是否会用到这个值
        /// </summary>
        public virtual string IS_CLIENT_USE { get; set; }
        /// <summary>
        /// 值类型(1:字符串  ;2:数字;3:布尔型;4:浮点数;)
        /// </summary>
        public virtual string VALUE_TYPE { get; set; }

        /// <summary>
        /// 分组内排序字段
        /// </summary>
        public virtual decimal SORT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string SYS_APP_KEY { get; set; }

    }
}
