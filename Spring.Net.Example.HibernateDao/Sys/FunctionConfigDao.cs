using System;
using System.Collections.Generic;
using NHibernate;

namespace Spring.Net.Example.HibernateDao.Sys
{
    //using Data.NHibernate.Support;
    using IDao.Sys;
    using Model.Dto.Config;
    using Model.Table;

    public class FunctionConfigDao : NHibernateDAO<CFG_FUNC>, IFunctionConfigDao
    {
        public bool ConfigIsEdit(string key)
        {
            throw new NotImplementedException();
        }

        public IList<CFG_FUNC> GetAllParamConfig()
        {
            //var sql = @"select id,
            //               group_name groupname,
            //               name,
            //               remark,
            //               value,
            //               a.key,
            //               is_edit isedit,
            //               control_type type,
            //               verify,
            //               is_client_use isclientuse,
            //               group_sort page,
            //               value_type valuetype
            //          from cfg_func a where a.group_name = '公共配置' ";
            //return QuerySession.ExecuteSqlString<ParamResult>(sql, appKey).ToList();

            return this.Where(" 1=1 and group_name='公共配置' ");
        }

        public IList<CFG_FUNC> GetParamConfig()
        {
            throw new NotImplementedException();
        }
    }
}
