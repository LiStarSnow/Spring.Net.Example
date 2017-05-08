using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.IBLL.Shared
{
    public interface IUserAllotService
    {
        bool ValidateMenuAllot(string userId, string[] validCode);

        bool ValidateUserLoginKey(string userId, string loginKey);
    }
}
