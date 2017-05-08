using Spring.Net.Example.IDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Net.Example.Model;

namespace Spring.Net.Example.Dao
{
    using Data.Generic;
    using Data.Common;
    using Model.Table;
    using Common;

    public class UserDao : AdoDaoSupport, IUserDao
    {
        public bool AddOrUpdateUser(FM_USER user)
        {
            return false;
        }

        public bool AddUser(FM_USER user)
        {
            return false;
        }

        public bool DeleteUser(FM_USER user)
        {
            return false;
        }

        public IList<FM_USER> GetAllUsers()
        {
            IList<FM_USER> res = new List<FM_USER>();

            string sql = "SELECT * FROM FM_USER";

            return AdoTemplate.QueryWithRowMapperDelegate<FM_USER>(System.Data.CommandType.Text, sql, delegate (System.Data.IDataReader dataReader, int rowNum)
            {
                return dataReader.ConvertToEntity<FM_USER>();
            }).ToList();
        }

        public FM_USER GetUserById(int Id)
        {
            return null;
        }

        public bool UpdateUser(FM_USER user)
        {
            return false;
        }

        public bool ValidateUserLoginKey(string userId, string loginKey)
        {
            var sql = @"select 1 from fm_user su where id=:userId and login_key=:loginkey";//select 1 from fm_user su where id=:userId and login_key=:loginkey
            IDbParameters dbparams = CreateDbParameters();
            dbparams.AddWithValue("userId", userId).DbType = System.Data.DbType.String;
            dbparams.AddWithValue("loginkey", loginKey);

            var res = AdoTemplate.ExecuteScalar(System.Data.CommandType.Text, sql, dbparams);

            return res == null ? false : true;

        }
    }
}
