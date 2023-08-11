using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Models
{
    public class KullaniciDetayConfiguration : IEntityTypeConfiguration<kullanicidetay>
    {
        public void Configure(EntityTypeBuilder<kullanicidetay> builder)
        {
            builder.ToTable("kullanıcıdetay");

            builder.Property(x => x.kullaniciid).HasColumnName("kullanıcıid");
        }
    }
}
