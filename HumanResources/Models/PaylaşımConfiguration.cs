using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HumanResources.Models
{
    public class PaylaşımConfiguration : IEntityTypeConfiguration<paylasim>
    {
        public void Configure(EntityTypeBuilder<paylasim> builder)
        {
            builder.ToTable("paylasım");

            builder.Property(x => x.kulllaniciid).HasColumnName("kullanıcıid"); //birincisi paylsımdaki kullanıcı id ikinci olanda database de ki ismi
            builder.Property(x => x.baslik).HasColumnName("baslık");
        }
    }
}
