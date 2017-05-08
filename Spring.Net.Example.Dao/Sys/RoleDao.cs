using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.Dao.Sys
{
    using Core;
    using Data.Generic;
    using IDao.Sys;
    using System.Data;
    using Common;
    using Data.Common;
    using Model.Dto.Sys.Common;

    public class RoleDao : AdoDaoSupport, IRoleDao
    {
        public bool ExistRoleName(string id, string name, string appKey)
        {
            IDbParameters dbParams = CreateDbParameters();
            dbParams.AddWithValue("id", id);
            dbParams.AddWithValue("name", name);
            dbParams.AddWithValue("appKey", appKey);
            var sql = @" select count(1) from fm_role where role_name =:name and sys_app_key =:appKey and id<>:name";

            return (decimal)AdoTemplate.ExecuteScalar(CommandType.Text, sql, dbParams) > 0;
        }

        public string GetRoleIdByName(string name, string appKey)
        {
            IDbParameters dbParams = CreateDbParameters();
            dbParams.AddWithValue("name", name);
            dbParams.AddWithValue("appKey", appKey);
            var sql = "select id from fm_role where role_name =:name and sys_app_key=:appKey ";

            var res = AdoTemplate.ExecuteScalar(CommandType.Text, sql, dbParams);

            return res == null ? string.Empty : res.ToString();
        }

        public List<RoleResult> GetRoles(List<string> appKey, string userId)
        {
            var sql = @"select sr.id,
                                   sr.role_name name,
                                   sr.remark,
                                   sr.state,
                                   sr.sys_app_key appkey
                              from fm_role sr where 1=1 ";

            if (null != appKey && appKey.Any())
            {
                sql += string.Format(" and sr.sys_app_key in ({0}) ", appKey.DataBaseStr());
            }
            if (!string.IsNullOrWhiteSpace(userId))
            {
                sql += string.Format(" and exists(select count(1) from fm_user_role where user_id='{0}' and role_id=sr.id) ", userId);
            }

            return AdoTemplate.QueryWithRowMapperDelegate(CommandType.Text, sql, delegate (IDataReader dataReader, int rowNum)
            {
                return dataReader.ConvertToEntity<RoleResult>();
            }).ToList();
        }
    }
}
