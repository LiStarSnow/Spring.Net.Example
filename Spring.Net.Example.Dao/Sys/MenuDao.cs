using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.Dao.Sys
{
    using Common;
    using Core;
    using Data.Common;
    using Data.Generic;
    using IDao.Sys;
    using Model.Sys.Menu;
    using Spring.Dao;
    using System.Data;

    public sealed class MenuDao : AdoDaoSupport, IMenuDao
    {

        public List<MenuResult> GetMenus(List<string> appkey, List<string> operationTypes, List<string> menuTypes)
        {
            var cmdText = @"select sm.id,
                       sm.menu_name name,
                       sm.parent_id   parentid,
                       sm.menu_view   menuview,
                       sm.menu_type   menutype,
                       sm.state   state,
                       sm.view_params viewparams,
                       sm.operation_type operationtype,
                       sm.handler,
                       sm.icon,
                       sm.sort,
                       sm.sys_app_key sysappkey,
                       sm.remark remark,
                       sm.validate_code validatecode
                  from fm_menu sm
                 where 1=1 ";

            if (appkey.Count > 0)
            {
                cmdText += string.Format(" and sys_app_key in ({0}) ", appkey.DataBaseStr());
            }
            if (operationTypes.Count > 0)
            {
                cmdText += string.Format(" and Operation_type in ({0}) ", operationTypes.DataBaseStr());
            }
            if (menuTypes.Count > 0)
            {
                cmdText += string.Format(" and (operation_type<>'2' or menu_type in({0}))", menuTypes.DataBaseStr());
            }

            cmdText += " order by sort asc";

            return AdoTemplate.QueryWithRowMapperDelegate<MenuResult>(CommandType.Text, cmdText, delegate (IDataReader dataReader, int rowNum)
            {
                return dataReader.ConvertToEntity<MenuResult>();
            }).ToList();
        }

        /// <summary>
        /// 获取用户菜单权限
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="appKey">系统编码</param>
        /// <param name="operationTypes">菜单操作类型(1:目录2:菜单3:功能)</param>
        /// <param name="menuTypes">菜单类型(1:业务菜单2:系统菜单3:公共菜单4:管理员菜单)</param>
        /// <returns></returns>
        public List<MenuResult> GetMenus(string userId, List<string> appKey, List<string> operationTypes, List<string> menuTypes)
        {
            var cmdText = @"
                select distinct sm.id,
                                sm.menu_name      name,
                                sm.parent_id      parentid,
                                sm.menu_view      menuview,
                                sm.menu_type      menutype,
                                sm.state          state,
                                sm.view_params    viewparams,
                                sm.operation_type operationtype,
                                sm.handler,
                                sm.icon,
                                sm.sort,
                                sm.sys_app_key    sysappkey,
                                sm.remark         remark,
                                sm.validate_code  validatecode
                  from fm_menu sm
                  left join fm_role_menu rm
                    on sm.id = rm.menu_id
                  left join (select ur.user_id,ur.role_id from fm_user_role ur inner join fm_role fr on ur.role_id=fr.id and fr.state='1') ur
                    on rm.role_id = ur.role_id
                 where sm.state = '1' ";
            if (!string.IsNullOrWhiteSpace(userId))
            {
                cmdText += string.Format(" and (ur.user_id='{0}' or operation_type='1' or (operation_type='2' and menu_type='3')) ", userId);
            }
            if (null != appKey && appKey.Any())
            {
                cmdText += string.Format(" and sys_app_key in ({0}) ", appKey.DataBaseStr());
            }
            if (null != operationTypes && operationTypes.Any())
            {
                cmdText += string.Format(" and Operation_type in ({0}) ", operationTypes.DataBaseStr());
            }
            if (null != menuTypes && menuTypes.Any())
            {
                cmdText += string.Format("  and (operation_type<>'2' or menu_type in({0}))", menuTypes.DataBaseStr());
            }

            cmdText += " order by sort asc";
            return AdoTemplate.QueryWithRowMapperDelegate<MenuResult>(CommandType.Text, cmdText, delegate (IDataReader dataReader, int rowNum)
            {
                return dataReader.ConvertToEntity<MenuResult>();
            }).ToList();
        }

        public decimal GetMenu(string appKey, decimal parentId, string menuName)
        {
            string cmdText = "select id from fm_menu where sys_app_key=:appKey and parent_id=:parentId and menu_name=:menuName ";
            IDbParameters dbParams = CreateDbParameters();
            dbParams.AddWithValue("appKey", appKey);
            dbParams.AddWithValue("parentId", parentId);
            dbParams.AddWithValue("menuName", menuName);

            return (decimal)AdoTemplate.ExecuteScalar(CommandType.Text, cmdText, dbParams);
        }

        public bool ExistMenu(decimal id, string appKey, decimal parentId, string menuName)
        {
            string cmdText = "select id from fm_menu where sys_app_key=:appKey and parent_id=:parentId and menu_name=:menuName and id<>:id ";
            IDbParameters dbParams = CreateDbParameters();
            dbParams.AddWithValue("id", id);
            dbParams.AddWithValue("appKey", appKey);
            dbParams.AddWithValue("parentId", parentId);
            dbParams.AddWithValue("menuName", menuName);

            return AdoTemplate.ExecuteScalar(CommandType.Text, cmdText, dbParams) != null;
        }
    }
}
