using Microsoft.EntityFrameworkCore;

namespace AreaCarouselOrnek.Data
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options)
        {

        }

        public DbSet<Slayt> Slaytlar { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Slayt>().HasData(
                new Slayt() { Id = 1, ResimYolu = "yagmur.jpg", Baslik = "Yagmur", Aciklama = "Hepimiz birer yağmur damlasıyız aslında", Sira = 1 },
                new Slayt() { Id = 2, ResimYolu = "manzara.jpg", Baslik = "Manzara", Aciklama = "Eğer sen doğaya zarar vermezsen doğa sana asla zarar vermez", Sira = 2 },
                new Slayt() { Id = 3, ResimYolu = "gokkusagi.jpg", Baslik = "Gökkusagi", Aciklama = "Rengarenk gökkuşağını görmek için yağmuru doyasıya yaşamak gerekir", Sira = 3 }
                );
        }
    }
}
