using Microsoft.EntityFrameworkCore;

namespace HumanResources.Models;

public class Databasecontext : DbContext
{
    public DbSet<kategori> kategori { get; set; }
    public DbSet<kullanici> kullanici { get; set; }
    public DbSet<kullanicidetay> kullanicidetay { get; set; }
    public DbSet<paylasim> paylasim { get; set; }
    public DbSet<logs> logs { get; set; }
    public DbSet<rol> rol { get; set; }
    public DbSet<basvuruKontrol> basvuruKontrol { get; set; }
    public Databasecontext(DbContextOptions<Databasecontext> options)
        : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
