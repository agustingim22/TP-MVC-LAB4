namespace TPFINALLAB4MVC.Models
{
    public class Partido
    {
        public int Id { get; set; }
        public string fecha { get; set; }
        public string NickNameRival { get; set; }
        public int IdJugador { get; set; }
        public Jugador? jugador { get; set; }
        public int IdEstado { get; set; }
        public Estado? estado { get; set; }
    }
}
