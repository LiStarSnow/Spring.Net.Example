using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.Model.Table
{
    [Serializable]
    public class FM_USER
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual Int32 ID { get; set; }

        /// <summary>
        /// 用户代码
        /// </summary>
        public virtual string USER_CODE { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string USER_NAME { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public virtual string USER_PASSWORD { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public virtual string USER_TELEPHONE { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public virtual string USER_EMAIL { get; set; }
        /// <summary>
        /// 用户类型（0：海虹人员 ,1：医保人员 2：医院人员3：参保人员）
        /// </summary>
        public virtual string USER_TYPE { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public virtual int SORT { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string REMARK { get; set; }
        /// <summary>
        /// 停用标志0.停用 1.正常
        /// </summary>
        public virtual string ENABLE_FLAG { get; set; }

        /// <summary>
        /// 是否内置用户
        /// </summary>
        public virtual string ISSYS_FLAG { get; set; }

        /// <summary>
        /// 登录错误连续次数
        /// </summary>
        public virtual int ERROR_TIMES { get; set; }
        /// <summary>
        /// 最后一次错误登录时间
        /// </summary>
        public virtual DateTime LAST_ERROR_TIME { get; set; }
        /// <summary>
        /// 最后一次更新时间
        /// </summary>
        public virtual DateTime LATEST_UPDATE_TIME { get; set; }
        /// <summary>
        /// 机构ID
        /// </summary>
        public virtual string ORG_ID { get; set; }
        /// <summary>
        /// 有效开始日期
        /// </summary>
        public virtual DateTime STARTDATE { get; set; }
        /// <summary>
        /// 有效截至日期
        /// </summary>
        public virtual DateTime EXPIRYDATE { get; set; }

        /// <summary>
        /// 最近密码修改时间
        /// </summary>
        public virtual DateTime LATEST_PASSWORD_UPDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string LOGIN_KEY { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public virtual IList<FM_ROLE> Roles { get; set; }

        public FM_USER()
        {
            Roles = new List<FM_ROLE>();
        }

        public virtual void AddRole(FM_ROLE role)
        {
            role.Users.Add(this);
            Roles.Add(role);
        }

    }
}
