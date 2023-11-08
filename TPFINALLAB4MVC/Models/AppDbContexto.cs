using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TPFINALLAB4MVC.Models
{
    public class AppDbContexto : IdentityDbContext<IdentityUser>
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
