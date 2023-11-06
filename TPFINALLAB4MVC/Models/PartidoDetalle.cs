namespace TPFINALLAB4MVC.Models
{
    public class PartidoDetalle
    {
        public int Id { get; set; }
        public int golesJugador { get; set; }
        public int golesRival { get; set; }
        public int cantRojas { get; set; }
        public int cantAmarillas { get; set; }
        public int IdPartido { get; set; }
        public Partido? Partido { get; set; }
    }
}
