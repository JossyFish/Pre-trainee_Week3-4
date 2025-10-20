using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WK_34.Entities;

namespace WK_34.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(a => a.Author)
                .WithMany(b => b.Books)
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
            }
    }
}
