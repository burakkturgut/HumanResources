using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Models
{
    public class KullaniciConfiguration : IEntityTypeConfiguration<Kullanici>
    {
        public void Configure(EntityTypeBuilder<Kullanici> builder)
        {
            builder.ToTable("Kullanıcı"); // bu ksımımız postgre Sql ile aynı olmak zorunda

            builder.Property(x => x.adi).HasColumnName("adı");
            builder.Property(x => x.soyadi).HasColumnName("soyadı");

        }
    }

}
