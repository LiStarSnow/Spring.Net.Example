using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.Dao.Sys
{
    using IDao.Sys;
    using Model.Dto.Config;
    using Data.Generic;
    using Data.Common;
    using Common;

    public class FunctionConfigDao : AdoDaoSupport, IFunctionConfigDao
    {
        /// <summary>
        /// 获取配置项是否可以修改
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ConfigIsEdit(string key, string appKey)
        {
            var sql = "select count(1) from cfg_func where key=:key and is_edit=1 and (sys_app_key=:appkey  or sys_app_key='0')";
            IDbParameters dbparams = CreateDbParameters();
            dbparams.AddWithValue("key", key).DbType = System.Data.DbType.String;
            dbparams.AddWithValue("appkey", appKey);


            return (decimal)AdoTemplate.ExecuteScalar(System.Data.CommandType.Text, sql, dbparams) == 1;
        }

        /// <summary>
        /// 获取系统配置
        /// </summary>
        /// <returns></returns>
        public List<ParamResult> GetAllParamConfig(string appKey)
        {
            var sql = @"select id,
                           group_name groupname,
                           name,
                           remark,
                           case
                             when b.value is not null then
                              b.value
                             else
                              a.value
                           end value,
                           a.key,
                           is_edit isedit,
                           control_type type,
                           verify,
                           is_client_use isclientuse,
                           group_sort page,
                           value_type valuetype
                      from cfg_func a
                      left join cfg_area_func b
                        on a.key = b.key
                       and b.sys_app_key=:sysappkey
                     where a.sys_app_key =:sysappkey
                        or a.group_name = '公共配置'";

            IDbParameters dbparams = CreateDbParameters();
            dbparams.AddWithValue("sysappkey", appKey).DbType = System.Data.DbType.String;

            var res = AdoTemplate.QueryWithRowMapperDelegate<ParamResult>(System.Data.CommandType.Text, sql, delegate (System.Data.IDataReader dataReader, int rowNum)
            {
                return dataReader.ConvertToEntity<ParamResult>();
            }, dbparams);

            return res.ToList();
        }

        /// <summary>
        /// 获取系统配置
        /// </summary>
        /// <returns></returns>
        public List<ParamResult> GetParamConfig(string appKey)
        {
            var sql = @"select id,
                               group_name groupname,
                               name,
                               remark,
                               case
                                 when b.value is not null then
                                  b.value
                                 else
                                  a.value
                               end value,
                               a.key,
                               is_edit isedit,
                               control_type type,
                               group_sort type,
                               verify,
                               is_client_use isclientuse,
                               group_sort page,
                               value_type valuetype
                          from cfg_func a
                          left join cfg_area_func b
                            on a.key = b.key
                           and b.sys_app_key = :sysappkey
                         where  a.is_display= '1' and (a.sys_app_key =:sysappkey
                            or a.group_name = '公共配置') order by group_sort asc,sort asc";

            IDbParameters dbparams = CreateDbParameters();
            dbparams.AddWithValue("sysappkey", appKey).DbType = System.Data.DbType.String;

            return AdoTemplate.QueryWithRowMapperDelegate<ParamResult>(System.Data.CommandType.Text, sql, delegate (System.Data.IDataReader dataReader, int rowNum)
            {
                return dataReader.ConvertToEntity<ParamResult>();
            }, dbparams).ToList();
        }
    }
}
