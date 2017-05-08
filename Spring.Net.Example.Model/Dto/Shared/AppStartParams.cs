using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.Model.Dto.Shared
{
    public class AppStartParams
    {
        /// <summary>
        /// 程序物理路径
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        /// Dto程序集名称列表
        /// </summary>
        public List<string> DtoAssemblys { get; set; } = new List<string>();

        /// <summary>
        /// 前端App名称
        /// </summary>
        public string WebAppName { get; set; }
    }
}
