using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Entity;

namespace MusicStreamer.infrastructure
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=musicstreamer.db");
        }

        public DbSet<UsuarioEntity> Usuarios { get; set; }
        public DbSet<AssinaturaEntity> Assinaturas { get; set; }


    }
}
