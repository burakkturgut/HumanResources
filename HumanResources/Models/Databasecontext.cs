using Microsoft.EntityFrameworkCore;

namespace HumanResources.Models;

public class Databasecontext : DbContext
{
    public DbSet<kategori> kategori { get; set; }
    public DbSet<Kullanici> kullanıcı { get; set; }
    public DbSet<kullanicidetay> kullanıcıdetay { get; set; }
    public DbSet<paylasim> paylasım { get; set; }

    public Databasecontext(DbContextOptions<Databasecontext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) //ovveride edilen bir ürün bir kre yazılır
    {
        modelBuilder.ApplyConfiguration(new KategoriConfiguration());
        modelBuilder.ApplyConfiguration(new PaylaşımConfiguration());
        modelBuilder.ApplyConfiguration(new KullaniciConfiguration());
        modelBuilder.ApplyConfiguration(new KullaniciDetayConfiguration());


        base.OnModelCreating(modelBuilder);
    }
    
}
