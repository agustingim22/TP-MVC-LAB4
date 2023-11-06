using TPFINALLAB4MVC.Models;

namespace TPFINALLAB4MVC.ViewsModels
{
    public class PartidosViewModel
    {
        public List<Partido> partidos { get; set; }
        public List<Jugador> jugadores { get; set; }
        public List<Estado> estados { get; set; }
        public int? fecha { get; set; }
        public string NickNameRival { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }

        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalItems / PageSize);
            }
        }
    }
}
