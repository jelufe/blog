using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
    public class VisualizationConfiguration : IEntityTypeConfiguration<Visualization>
    {
        public void Configure(EntityTypeBuilder<Visualization> builder)
        {
            builder.ToTable("Visualization");
        }
    }
}
