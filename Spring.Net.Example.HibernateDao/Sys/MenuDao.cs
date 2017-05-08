using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace Spring.Net.Example.HibernateDao.Sys
{
    //using Data.NHibernate.Support;
    using IDao.Sys;
    using Model.Dto.Sys.Common;
    using Model.Sys.Menu;

    public class MenuDao : IMenuDao
    {

        public bool ExistMenu(decimal id, string appKey, decimal parentId, string menuName)
        {
            return true;
        }

        //public decimal GetMenu(string appKey, decimal parentId, string menuName)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<MenuResult> GetMenus(List<string> appkey, List<string> operationTypes, List<string> menuTypes)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<MenuResult> GetMenus(string userId, List<string> appKey, List<string> operationTypes, List<string> menuTypes)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
