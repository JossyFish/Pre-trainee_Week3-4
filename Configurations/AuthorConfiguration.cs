using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WK_34.Entities;

namespace WK_34.Configurations
{
    public partial class AuthorConfiguration : IEntityTypeConfiguration<AuthorEntity>
    {
        public void Configure(EntityTypeBuilder<AuthorEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired();

        }
    }
}
