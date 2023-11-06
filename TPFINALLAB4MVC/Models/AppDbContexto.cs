using Microsoft.EntityFrameworkCore;

namespace TPFINALLAB4MVC.Models
{
    public class AppDbContexto : DbContext
    {
        public AppDbContexto(DbContextOptions<AppDbContexto> options) : base(options)
        {

        }


        public DbSet<Partido> partidos { get; set; }
        public DbSet<Jugador> jugadores { get; set; }
        public DbSet<Estado> estados { get; set; }
        public DbSet<PartidoDetalle> partidosDetalles { get; set; }
    }
}
