using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cis.Fm.Model.Dto.Sys.User
{
    public class AllotResult
    {
        public string Id { get; set; }

        public string ClassId { get; set; }

        public string ClassName { get; set; }

        public string ItemId { get; set; }

        public string ItemName { get; set; }

        public string State { get; set; }

        public bool IsChecked { get; set; }
    }
}
