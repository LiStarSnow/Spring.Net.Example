using System.Data.Entity.Migrations;

namespace Spring.Net.Example.EFDao
{
    internal class MigrationsConfiguration : DbMigrationsConfiguration<EFContext>
    {
        public MigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}