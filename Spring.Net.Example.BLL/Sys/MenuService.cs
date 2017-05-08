using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.BLL.Sys
{
    using IBLL.Sys;
    using IDao.Sys;

    public class MenuService : IMenuService
    {
        public IMenuDao MenuDao { get; set; }

        public bool ExistMenu(decimal id, string appKey, decimal parentId, string menuName)
        {
            return MenuDao.ExistMenu(id, appKey, parentId, menuName);
        }

        //public decimal GetMenu(string appKey, decimal parentId, string menuName)
        //{
        //    return MenuDao.GetMenu(appKey, parentId, menuName);
        //}

        //public List<MenuResult> GetMenus(List<string> appkey, List<string> operationTypes, List<string> menuTypes)
        //{
        //    return MenuDao.GetMenus(appkey, operationTypes, menuTypes);
        //}

        //public List<MenuResult> GetMenus(string userId, List<string> appKey, List<string> operationTypes, List<string> menuTypes)
        //{
        //    return MenuDao.GetMenus(userId, appKey, operationTypes, menuTypes);
        //}
    }
}
