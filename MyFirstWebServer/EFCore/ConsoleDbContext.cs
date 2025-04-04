using Microsoft.EntityFrameworkCore;

namespace ConsoleAppWithEFCore
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=EFCoreExampleDB.db");
        }
    }
}