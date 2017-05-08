using Spring.Net.Example.Model.Dto.Config;
using Spring.Net.Example.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.IDao.Sys
{
    public interface IFunctionConfigDao : IDAO<CFG_FUNC>
    {
        IList<CFG_FUNC> GetParamConfig();

        IList<CFG_FUNC> GetAllParamConfig();

        bool ConfigIsEdit(string key);
    }
}
