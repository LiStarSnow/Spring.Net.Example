using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spring.Net.Example.EFDao.Configurations
{
    using Model.Table;
    public class UserConfiguration : ConfigurationBase<FM_USER>
    {
        public UserConfiguration()
        {
            this.HasKey(i => i.ID);
            this.Property(i => i.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).IsOptional();
        }
    }
}
