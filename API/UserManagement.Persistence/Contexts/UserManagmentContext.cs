using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Models;
using UserManagement.Persistence.Mappers;

namespace UserManagement.Persistence.Contexts
{
    public class UserManagementContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserManagementContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<User>(new UserMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
