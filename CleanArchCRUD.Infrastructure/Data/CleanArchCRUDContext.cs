using CleanArchCRUD.Domain.Entities;
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

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });
        }
    }
}
