/* ***********************************************
 * author :  汪振
 * function: 用户管理列表
 * history:  created by 汪振 2015-07-17
 * ***********************************************/

namespace Cis.Fm.Model.Dto.Sys.User
{
    using System;
    using Cis.DbLight.TableMetadata;
    using System.ComponentModel.DataAnnotations;
    using Enum;

    /// <summary>
    /// 用户管理列表查看
    /// </summary>
    public class UserResult
    {

        /// <summary>
        /// 默认issysFlag=‘0’
        /// </summary>
        private string isSysFlag = "0";

        /// <summary>
        /// 主键
        /// </summary>
        [Alias("ID")]
        public string Id { get; set; }

        /// <summary>
        /// 登录名称
        /// </summary>
        [Alias("USER_CODE")]
        [Required(ErrorMessage = "用户登录名称是必需条件")]
        [RegularExpression(@"^[a-zA-Z0-9_]{3,16}$",
            ErrorMessage = "用户登录名称由以 字母开头的 3-16位 字母数字下划线组成")]
        public string Code { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Alias("USER_NAME")]
        [Required(ErrorMessage = "用户名称是必需条件")]
        [RegularExpression(@"^[\u4e00-\u9fa5\w,?:]{0,}$", ErrorMessage = "用户名称不能包含特殊字符")]
        public string Name { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [Alias("USER_PASSWORD")]
        public string Password { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Alias("USER_TELEPHONE")]
        [RegularExpression(@"(\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$",
            ErrorMessage = "请输入正确的电话或者手机号码")]
        public string Telephone { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [Alias("USER_EMAIL")]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$",
            ErrorMessage = "请输入正确的电子邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Alias("SORT")]
        public decimal? Sort { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Alias("REMARK")]
        [RegularExpression(@"^[\u4e00-\u9fa5\w,?:]{0,}$", ErrorMessage = "备注不能包含特殊字符")]
        public string Remark { get; set; }

        /// <summary>
        /// 状态（1：正常0：禁用）
        /// </summary>
        [Alias("ENABLE_FLAG")]
        public string EnableFlag { get; set; }

        public string UserType { get; set; }

        /// <summary>
        /// 是否内置角色（1：是0：否）
        /// </summary>
        [Alias("ISSYS_FLAG")]
        public string IsSysFlag
        {
            get
            {
                return this.isSysFlag;
            }
            set
            {
                this.isSysFlag = value;
            }
        }

        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgId { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// 登录密钥
        /// </summary>
        public string LoginKey { get; set; }

        /// <summary>
        /// 最近密码修改时间
        /// </summary>
        public DateTime? LatestPassUpTime { get; set; }

        /// <summary>
        /// 登录错误连续次数
        /// </summary>
        public decimal? ErrorTimes { get; set; }

        /// <summary>
        /// 最后一次错误登录时间
        /// </summary>
        public DateTime? LastErrorTime { get; set; }
    }
}
