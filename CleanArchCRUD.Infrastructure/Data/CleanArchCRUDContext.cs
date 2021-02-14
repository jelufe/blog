using CleanArchCRUD.Domain.Entities;
using CleanArchCRUD.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CleanArchCRUD.Infrastructure.Data
{
    public partial class CleanArchCRUDContext : DbContext
    {
        public CleanArchCRUDContext()
        {
        }

        public CleanArchCRUDContext(DbContextOptions<CleanArchCRUDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
