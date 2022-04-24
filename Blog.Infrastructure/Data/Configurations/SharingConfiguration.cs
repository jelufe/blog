using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
    public class SharingConfiguration : IEntityTypeConfiguration<Sharing>
    {
        public void Configure(EntityTypeBuilder<Sharing> builder)
        {
            builder.ToTable("Sharing");
        }
    }
}
