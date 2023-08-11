using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HumanResources.Models
{
    public class KategoriConfiguration : IEntityTypeConfiguration<kategori>
    {
        public void Configure(EntityTypeBuilder<kategori> builder)
        {
            builder.ToTable("kategori");

            builder.Property(x => x.kategoriadi).HasColumnName("kategoriadı");
        }
    }
}
