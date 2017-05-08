using NHibernate;
using NHibernate.Linq;
using Spring.Net.Example.IDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spring.Net.Example.HibernateDao
{
    public class NHibernateDAO<TEntity> : IDAO<TEntity> where TEntity : class
    {
        private ISession Session = NHibernateHelper.GetCurrentSession();

        /// <summary>  
        /// 当前实体对应的表名  
        /// </summary>  
        public string TableName
        {
            get
            {
                //((TableAttribute)((TableAttribute[])(typeof(TEntity).GetCustomAttributes(typeof(TableAttribute), true)))[0]).Name
                var attrs = typeof(TEntity).GetCustomAttributes(typeof(TableAttribute), true);
                if (attrs.Any())
                {
                    return ((TableAttribute)attrs[0]).Name;
                }
                else
                {
                    return typeof(TEntity).ToString().Substring(typeof(TEntity).ToString().LastIndexOf('.') + 1);
                }
            }
        }
        /// <summary>  
        /// 添加实体  
        /// </summary>  
        /// <param name="entity"></param>  
        public int Add(TEntity entity)
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                try
                {
                    int id = (int)Session.Save(TableName, entity);
                    Session.Flush();
                    transaction.Commit();
                    return id;
                }
                catch (HibernateException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>  
        /// 修改实体  
        /// </summary>  
        /// <param name="entity"></param>  
        public void Update(TEntity entity)
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                try
                {
                    Session.Update(entity);
                    Session.Flush();
                    transaction.Commit();
                }
                catch (HibernateException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        /// <summary>  
        /// 保存或修改实体  
        /// </summary>  
        /// <param name="customer"></param>  
        public void SaveOrUpdate(IList<TEntity> list)
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                try
                {
                    foreach (var entity in list)
                    {
                        Session.SaveOrUpdate(entity);
                    }
                    Session.Flush();
                    transaction.Commit();
                }
                catch (HibernateException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>  
        /// 删除实体  
        /// </summary>  
        /// <param name="entity"></param>  
        public void Delete(TEntity entity)
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                try
                {
                    Session.Delete(entity);
                    Session.Flush();
                    transaction.Commit();
                }
                catch (HibernateException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>  
        /// 按条件删除  
        /// </summary>  
        /// <param name="sqlWhere">删除条件</param>  
        public void Delete(string sqlWhere)
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                try
                {
                    Session.Delete(string.Format("from {0} Where {1}", TableName, sqlWhere));
                    Session.Flush();
                    transaction.Commit();
                }
                catch (HibernateException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>  
        /// 根据ID得到实体  
        /// </summary>  
        /// <param name="id"></param>  
        /// <returns></returns>  
        public TEntity Get(int id)
        {
            return Session.Get<TEntity>(id);
        }
        /// <summary>  
        /// 根据ID得到实体  
        /// </summary>  
        /// <param name="id"></param>  
        /// <returns></returns>  
        public TEntity Load(int id)
        {
            return Session.Load<TEntity>(id);
        }
        /// <summary>  
        /// 得到所有实体  
        /// </summary>  
        /// <returns></returns>  
        public IList<TEntity> LoadAll()
        {
            return Session.Query<TEntity>().ToList();
        }

        /// <summary>  
        /// 按条件排序得到前N条记录  
        /// </summary>  
        /// <param name="top">获取条数</param>  
        /// <param name="field">排序字段</param>  
        /// <param order="field">排序方式，升序asc,降序desc</param>  
        /// <returns></returns>  
        public IList<TEntity> QueryTop(int top, string field, string order)
        {
            if (order == "asc")
            {
                return Session.CreateCriteria(typeof(TEntity)).SetMaxResults(top).AddOrder(NHibernate.Criterion.Order.Asc(field)).List<TEntity>();
            }
            else
            {
                return Session.CreateCriteria(typeof(TEntity)).SetMaxResults(top).AddOrder(NHibernate.Criterion.Order.Desc(field)).List<TEntity>();
            }
        }

        /// <summary>  
        /// 根据条件得到实体  
        /// </summary>  
        /// <param name="sqlWhere">查询条件</param>  
        /// <returns></returns>  
        public IList<TEntity> Where(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder(string.Format("from {0} c", TableName));
            if (!string.IsNullOrEmpty(sqlWhere))
            {
                strSql.Append(string.Format(" where {0}", sqlWhere));
            }
            return Session.CreateQuery(strSql.ToString()).List<TEntity>();
        }

        /// <summary>  
        /// 得到统计数量  
        /// </summary>  
        /// <param name="strWhere">查询条件</param>  
        /// <returns></returns>  
        public int GetRecordCount(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder(string.Format("select count(1) from {0} c", TableName));
            if (!string.IsNullOrEmpty(sqlWhere))
            {
                strSql.Append(string.Format(" where {0}", sqlWhere));
            }
            return (int)Session.CreateSQLQuery(strSql.ToString()).UniqueResult();
        }

        /// <summary>  
        /// 分页获取数据列表  
        /// </summary>  
        /// <param name="PageSize">每页获取数据条数</param>  
        /// <param name="PageIndex">当前页是第几页</param>  
        /// <param name="strWhere">查询条件</param>  
        /// <returns></returns>  
        public IList<TEntity> GetPageList(int PageSize, int PageIndex, string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(string.Format("select top {0} * from {1} where ID not in(select top  ", PageSize,
                TableName));
            strSql.Append(PageSize * (PageIndex - 1));
            strSql.Append(string.Format(" ID from {0}", TableName));
            if (!string.IsNullOrEmpty(sqlWhere))
            {
                strSql.Append(string.Format(" where {0} ) and {0}", sqlWhere));
            }
            else
            {
                strSql.Append(")");
            }
            return Session.CreateSQLQuery(strSql.ToString()).AddEntity(typeof(TEntity)).List<TEntity>();
        }



    }
}
