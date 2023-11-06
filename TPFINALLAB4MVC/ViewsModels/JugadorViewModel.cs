using TPFINALLAB4MVC.Models;

namespace TPFINALLAB4MVC.ViewsModels
{
    public class JugadorViewModel
    {
        public List<Jugador> jugadores { get; set; }
        public string nombre { get; set; }
        public int? edad { get; set; }
        public string nickName { get; set; }
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
