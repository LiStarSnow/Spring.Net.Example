using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.Dao.Sys
{
    using Core;
    using Data.Common;
    using Data.Generic;
    using IDao.Sys;
    using Model.Dto.Sys.Common;
    using System.Data;
    using Common;
    using Oracle.ManagedDataAccess.Client;
    using System.Data.Common;

    public class UserRoleDao : AdoDaoSupport, IUserRoleDao
    {
        public List<UserDto> GetRoleUsers(string roleId)
        {
            var sql = "select user_code code,user_name name from fm_user_role ur join fm_user su on ur.user_id = su.id and ur.role_id=:roleId";
            return AdoTemplate.QueryWithRowMapperDelegate(CommandType.Text, sql, delegate (IDataReader dataReader, int rowNum)
            {
                return dataReader.ConvertToEntity<UserDto>();
            }, "roleId", DbType.String, 100, roleId).ToList();
        }

        public List<RoleResult> GetUserRoles(string userId, string userType, List<string> appKey)
        {
            var sql = @"
                select sr.id,
                       sr.role_name name,
                       fa.name appname,
                       sr.remark,
                       sr.state,
                       decode(ur.user_id, null, 'false', 'true') IsChecked
                  from fm_role sr 
                  inner join fm_application fa on sr.sys_app_key = fa.key and sr.state='1' and fa.application_type=:applicationType 
                  left join fm_user_role ur
                    on sr.id = ur.role_id and ur.user_id=:userId ";

            if (null != appKey && appKey.Count > 0)
            {
                sql += string.Format(" where sr.sys_app_key in ({0}) ", appKey.DataBaseStr());
            }
            IDbParameters DbParams = CreateDbParameters();
            DbParams.AddWithValue("userId", userId);
            DbParams.AddWithValue("applicationType", userType);
            return AdoTemplate.QueryWithRowMapperDelegate(System.Data.CommandType.Text, sql, delegate (IDataReader dataReader, int rowNum)
            {
                return dataReader.ConvertToEntity<RoleResult>();
            }, DbParams).ToList();
        }

        public bool Delete(string userId, List<string> appKey)
        {
            string sql = "DELETE fm_user_role WHERE user_id=:userId ";

            if (null != appKey && appKey.Any())
            {
                sql += string.Format(" and role_id in (select id from fm_role where sys_app_key in ({0}))", appKey.DataBaseStr());
            }

            return AdoTemplate.ExecuteNonQuery(CommandType.Text, sql, "userId", DbType.String, 100, userId) > 0;
        }

        public bool Insert(string userId, List<string> roleIds)
        {
            string sql = "INSERT INTO FM_USER_ROLE(ID,USER_ID,ROLE_ID) VALUES(SEQ_FM_USER_ROLE.nextval,:USER_ID,:ROLE_ID)";

            Dictionary<string, object[]> colRowData = new Dictionary<string, object[]>();
            colRowData.Add("USER_ID", roleIds.Select(str => (object)userId).ToArray());
            colRowData.Add("ROLE_ID", (from str in roleIds
                                       select (object)str).ToArray());

            OracleParameter[] dbParams = BatchOracleHelper.GetOracleParameters(colRowData);

            return AdoTemplate.Execute<bool>(delegate (DbCommand command)
             {
                 OracleCommand cmd = command as OracleCommand;

                 cmd.ArrayBindCount = colRowData.Values.First().Length; // 很重要
                 cmd.BindByName = true;
                 cmd.CommandType = CommandType.Text;
                 cmd.CommandText = sql;
                 cmd.CommandTimeout = 600; // 10分钟

                 cmd.Parameters.AddRange(dbParams);

                 return cmd.ExecuteNonQuery() > 0;
             });

        }

        public void RoleAllot(string userId, List<string> roleIds, List<string> appKey)
        {
            throw new NotImplementedException();
        }

        public bool ValidRoleHasUser(string roleId)
        {
            return (decimal)AdoTemplate.ExecuteScalar(CommandType.Text, "select count(1) from fm_user_role where role_id=:roleId", "roleId", DbType.String, 100, roleId) > 0;
        }
    }
}
