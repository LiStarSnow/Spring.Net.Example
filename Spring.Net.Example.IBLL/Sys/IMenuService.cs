using Spring.Net.Example.Model.Sys.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.IBLL.Sys
{
    public interface IMenuService
    {
        //List<MenuResult> GetMenus(List<string> appkey, List<string> operationTypes, List<string> menuTypes);

        //List<MenuResult> GetMenus(string userId, List<string> appKey, List<string> operationTypes, List<string> menuTypes);

        //decimal GetMenu(string appKey, decimal parentId, string menuName);

        bool ExistMenu(decimal id, string appKey, decimal parentId, string menuName);
    }
}
