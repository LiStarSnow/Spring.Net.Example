using Spring.Net.Example.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Net.Example.Model;
using Spring.Net.Example.IDao;
using Spring.Net.Example.Model.Table;
using BaseDto;
//using Spring.Transaction.Support;
using Spring.Net.Example.IDao.Sys;
using System.Collections;

namespace Spring.Net.Example.BLL
{
    public class UserService : IUserService
    {
        //public Spring.Transaction.IPlatformTransactionManager TransactionManager { get; set; }
        public IUserDao UserDao { get; set; }

        public IUserRoleDao UserRoleDao { get; set; }

        public IList<FM_USER> GetAllUsers()
        {
            //return this.UserDao.LoadAll();
            return this.UserDao.GetAllUsers();
        }

        public FM_USER GetUserById(int userId)
        {
            return this.UserDao.Get(userId);
        }

        public void SaveOrUpdate(IList<FM_USER> list)
        {
            this.UserDao.SaveOrUpdate(list);
        }

        public void Update(FM_USER user)
        {
            this.UserDao.Update(user);
        }

        public Response<bool> RoleAllot(string userId, List<string> roleIds)
        {
            Response<bool> res = new Response<bool>();

            //TransactionTemplate tt = new TransactionTemplate(TransactionManager);
            //res.Result = (bool)tt.Execute(delegate
            //{
            //    UserRoleDao.Delete(userId, Global.CurrentApp.AppKey);
            //    return UserRoleDao.Insert(userId, roleIds);
            //});

            return res;
        }

        public int Add(FM_USER entity)
        {
            return this.UserDao.Add(entity);
        }

        public void Delete(FM_USER entity)
        {
            this.UserDao.Delete(entity);
        }

        public void Delete(string sqlWhere)
        {
            this.UserDao.Delete(sqlWhere);
        }

        public FM_USER Get(int id)
        {
            return UserDao.Get(id);
        }

        public FM_USER Load(int id)
        {
            return UserDao.Load(id);
        }

        public IList<FM_USER> LoadAll()
        {
            return UserDao.LoadAll();
        }

        public IList<FM_USER> QueryTop(int top, string field, string order)
        {
            return UserDao.QueryTop(top, field, order);
        }

        public IList<FM_USER> Where(string sqlWhere)
        {
            return UserDao.Where(sqlWhere);
        }

        public int GetRecordCount(string strWhere)
        {
            return UserDao.GetRecordCount(strWhere);
        }

        public IList<FM_USER> GetPageList(int PageSize, int PageIndex, string strWhere)
        {
            return UserDao.GetPageList(PageSize, PageIndex, strWhere);
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
            return UserDao.GetPageList(PageSize, PageIndex, field, order, sqlWhere);
        }

        public IList Test()
        {
            return UserDao.Test();
        }
    }
}
