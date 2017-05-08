using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.EFDao.Configurations
{
    [InheritedExport(typeof(IConfiguration))]
    public abstract class ConfigurationBase<TEntity> : EntityTypeConfiguration<TEntity>, IConfiguration where TEntity : class  // where TEntity : EntityBase
    {
        protected ConfigurationBase()
        {
            this.ToTable(typeof(TEntity).Name);
            //this.HasKey(i => i.Id);

            //this.Property(i => i.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
        }

        public void Regist(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}
