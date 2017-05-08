using Spring.Net.Example.IDao.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Net.Example.Model.Dto.Sys.Role.Role;

namespace Spring.Net.Example.Dao.Sys
{
    using Common;
    using Core;
    using Data.Generic;
    using System.Data.Common;
    using System.Data;
    using Data.Common;

    public sealed class RoleMenuDao : AdoDaoSupport, IRoleMenuDao
    {
        /// <summary>
        /// 获取角色功能权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<RoleMenuResult> GetRoleFuncs(string id)
        {
            var sql = @"select sm.id,
                               sm.parent_id parentId,
                               sm.sort,
                               rm.role_id roleid,
                               sm.menu_name menuname,
                               sm.operation_type operationtype,
                               decode(rm.role_id,null,'0','1') ischecked
                          from fm_menu sm
                          left join fm_role_menu rm
                            on sm.id = rm.menu_id and rm.role_id = :id
                where sm.state = '1' and sm.sys_app_key=(select sys_app_key from fm_role where id=:id) and sm.operation_type = '3')
                         order by sm.sort asc";

            return AdoTemplate.QueryWithRowMapperDelegate<RoleMenuResult>(System.Data.CommandType.Text, sql, delegate (IDataReader dataReader, int rowNum)
            {
                return dataReader.ConvertToEntity<RoleMenuResult>();
            }).ToList();
        }

        /// <summary>
        /// 获取角色菜单功能权限
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns>返回角色菜单功能权限信息</returns>
        public List<RoleMenuResult> GetRoleMenus(string id)
        {
            var sql = @"select sm.id,
                               sm.parent_id parentId,
                               sm.sort,
                               rm.role_id roleid,
                               sm.menu_name menuname,
                               sm.remark remark,
                               sm.operation_type operationtype,
                               decode(rm.role_id,null,'0','1') ischecked
                          from fm_menu sm
                          left join fm_role_menu rm
                            on sm.id = rm.menu_id and rm.role_id = :id
                         where sm.state = '1' and sm.sys_app_key=(select sys_app_key from fm_role where id=:id) and (sm.operation_type <> '2' or sm.menu_type in ('1','2'))
                         order by sm.sort asc";

            IDbParameters dbParams = CreateDbParameters();
            dbParams.AddWithValue("id", id);

            //return AdoTemplate.QueryWithRowMapperDelegate<RoleMenuResult>(CommandType.Text, sql, delegate (IDataReader dataReader, int rowNum)
            //{
            //    return dataReader.ConvertToEntity<RoleMenuResult>();
            //}, dbParams).ToList();

            return AdoTemplate.QueryWithRowMapperDelegate<RoleMenuResult>(CommandType.Text, sql, delegate (IDataReader dataReader, int rowNum)
            {
                return dataReader.ConvertToEntity<RoleMenuResult>();
            }, "id", DbType.String, 32, id).ToList();
        }

        /// <summary>
        /// 判断用户验证码是否属于权限内
        /// </summary>
        /// <param name="userId">用户编码</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        public bool ValidateMenuAllot(List<string> appkey, string userId, List<string> code)
        {
            var cmdText = "select 1 from fm_user where issys_flag='1' and id=:userId";
            IDbParameters dbParams = CreateDbParameters();
            dbParams.AddWithValue("userId", userId);

            var res = AdoTemplate.ExecuteScalar(CommandType.Text, cmdText, dbParams);
            //内置用户权限不限制
            if (null != res && (decimal)res > 0)
            {
                return true;
            }

            cmdText = @"select count(1) from fm_role_menu rm 
inner join fm_user_role ur on rm.role_id = ur.role_id and ur.user_id = :userId 
inner join fm_role fr on fr.id=ur.role_id and fr.state='1' 
inner join fm_menu mn on rm.menu_id = mn.id ";

            if (null != appkey && appkey.Any())
            {
                cmdText += string.Format(" and mn.sys_app_key in ({0}) ", appkey.DataBaseStr());
            }
            if (null != code && code.Any())
            {
                cmdText += string.Format(" and mn.validate_code in ({0}) ", code.DataBaseStr());
            }
            //sql.AppendInWhereHasValue(() => appkey, " and mn.sys_app_key in ({0}) ");
            //if (code.Count == 1)
            //{
            //    sql.AppendWhereNotNull(() => code.First(), " and mn.validate_code = {0} ");
            //}
            //else
            //{
            //    sql.AppendInWhereHasValue(() => code, " and mn.validate_code in ({0}) ");
            //}
            AdoTemplate.ExecuteScalar(CommandType.Text, cmdText, dbParams);

            return (int)AdoTemplate.ExecuteScalar(CommandType.Text, cmdText, dbParams) > 0;
        }
    }
}
