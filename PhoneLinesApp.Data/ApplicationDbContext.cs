using PhoneLinesApp.Core.Models;
using System.Data.Entity;

namespace PhoneLinesApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=PhoneLinesDb")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<
                    ApplicationDbContext,
                    Migrations.Configuration>());
        }

        public DbSet<PhoneLine> PhoneLines { get; set; }
    }
}
