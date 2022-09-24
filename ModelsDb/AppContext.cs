using Microsoft.EntityFrameworkCore;

namespace ModelsDb
{
    public class AppContext : DbContext
    {
        public DbSet<ClientDb> Clients { get; set; }
        public DbSet<AccountDb> Accounts { get; set; }
        public DbSet<EmployeeDb> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;" +
                "Port = 5432;" +
                "Database=usersdb;" +
                "Username=postgres;" +
                "Password=password");
        }
    }
}
