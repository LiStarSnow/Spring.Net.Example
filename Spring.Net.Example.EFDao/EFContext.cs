using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.Composition;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Spring.Net.Example.EFDao
{
    using Model.Table;

    public class EFContext : DbContext
    {
        public EFContext(string connectionName)
        : base(connectionName)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EFContext, MigrationsConfiguration>());
        }

        [ImportMany(typeof(IConfiguration))]
        public IEnumerable<IConfiguration> Configurations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("STAR");
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            if (this.Configurations != null)
            {
                foreach (var configuration in this.Configurations)
                {
                    configuration.Regist(modelBuilder.Configurations);
                }
            }
        }

        //public DbSet<FM_USER> Users
        //{
        //    get;
        //    set;
        //}

        //public DbSet<FM_ROLE> Roles
        //{
        //    get; set;
        //}

        //public DbSet<FM_MENU> Menus
        //{
        //    get;
        //    set;
        //}

    }
}
