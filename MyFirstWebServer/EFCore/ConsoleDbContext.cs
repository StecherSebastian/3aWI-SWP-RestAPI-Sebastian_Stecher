using Microsoft.EntityFrameworkCore;

namespace EFCore
{
    public class ConsoleDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<City> Cities { get; set; } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "EFCoreExampleDB.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}