using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.HibernateDao
{
    using IDao;
    using Model.Table;
    using NHibernate;
    using NHibernate.Criterion;
    using System.Collections;

    //using NHibernate.Linq;
    public class UserHibernateDao : NHibernateDAO<FM_USER>, IUserDao
    {

        public bool AddOrUpdateUser(FM_USER user)
        {
            throw new NotImplementedException();
        }

        public bool AddUser(FM_USER user)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                session.Save(user);
                session.Flush();

                tx.Commit();
                return true;
            }
        }

        public bool DeleteUser(FM_USER user)
        {
            throw new NotImplementedException();
        }

        public IList Test()
        {
            ISession session = NHibernateHelper.GetCurrentSession();

            IList results = session.CreateCriteria(typeof(FM_USER))
.SetProjection(Projections.Alias(Projections.GroupProperty("ID"), "i"))
    .AddOrder(Order.Asc("i"))

    .List();

            return results;
        }

        public IList<FM_USER> GetAllUsers()
        {
            IList<FM_USER> res = new List<FM_USER>();
            #region  Spring.Data.Nhibernate 的查询实现
            //Common.Logging, Version=3.0.0.0, Culture=neutral, PublicKeyToken=af08829b84f0328e
            //return HibernateTemplate.ExecuteFind(delegate (NHibernate.ISession session)
            //{
            //    StringBuilder sb = new StringBuilder();
            //    sb.Append("from FM_USER ");
            //    IQuery query = session.CreateQuery(sb.ToString());
            //    //query.SetParameter(0, Id);
            //    return query.List<FM_USER>();
            //}, true);

            #endregion  Spring.Data.Nhibernate 的查询实现


            ISession session = NHibernateHelper.GetCurrentSession();

            #region 原生SQL关联查询
            /*
            return session.CreateSQLQuery(@"select {u.*} from FM_USER u")
.AddEntity("u", typeof(FM_USER)).List();
           */

            #endregion 原生SQL关联查询

            #region HQL关联查询

            //HQL关联查询  的查询方式才能查询出子对象Roles
            //return session.CreateQuery("select u from FM_USER u ")
            //    .List<FM_USER>();

            return session.CreateQuery("select DISTINCT u from FM_USER u LEFT JOIN u.Roles ")
                .List<FM_USER>();

            #endregion HQL关联查询

            //return session.CreateCriteria(typeof(FM_USER))
            //    .CreateCriteria("Roles")
            //    .Future<FM_USER>().ToList();

        }

        public bool UpdateUser(FM_USER user)
        {
            throw new NotImplementedException();
        }

        public bool ValidateUserLoginKey(string userId, string loginKey)
        {
            //NHibernate.ISession session = this.SessionFactory.GetCurrentSession();

            //var query = session.QueryOver<FM_USER>();

            //return (from user in query
            //        where user.ID.Equals(userId) && user.LOGIN_KEY.Equals(loginKey)
            //        select user.ID).Any();

            //return false;
            ISession session = NHibernateHelper.GetCurrentSession();
            //HQL
            IQuery query = session.CreateQuery("SELECT user.ID from FM_USER user where user.ID=:userId and user.LOGIN_KEY=:loginKey ");
            query.SetString("userId", userId);
            query.SetString("loginKey", loginKey);

            return query.SetFirstResult(1).SetMaxResults(10).Enumerable<FM_USER>().Any();

            //LINQ
            //return session.QueryOver<FM_USER>().Where(x => x.ID == userId && x.LOGIN_KEY == loginKey).Future().Any();

        }

        /// <summary>  
        /// 分页获取数据列表  
        /// </summary>  
        /// <param name="PageSize">每页获取数据条数</param>  
        /// <param name="PageIndex">当前页是第几页</param>  
        /// <param name="strWhere">查询条件</param>  
        /// <returns></returns>  
        public IList<FM_USER> GetPageList(int PageSize, int PageIndex, string field, string order, string sqlWhere)
        {
            ISession Session = NHibernateHelper.GetCurrentSession();

            var orderBySql = string.Empty;

            if (order == "asc")
            {
                orderBySql = string.Format(" order by {0} {1} ", field, "asc");
            }
            else
            {
                orderBySql = string.Format(" order by {0} {1} ", field, "desc");
            }
            var query = Session.CreateSQLQuery(string.Format("select * from {0} where 1=1 {1}  {2}", TableName, sqlWhere, orderBySql));

            return query.AddEntity(typeof(FM_USER)).SetFirstResult((PageIndex - 1) * PageSize).SetMaxResults(PageSize).List<FM_USER>();

            //return Session.CreateSQLQuery(strSql.ToString()).AddEntity(typeof(T)).List<T>();
        }
    }
}
