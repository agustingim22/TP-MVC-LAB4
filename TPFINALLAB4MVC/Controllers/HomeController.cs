using System.Diagnostics;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPFINALLAB4MVC.Models;

namespace TPFINALLAB4MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContexto _context;
        private readonly IWebHostEnvironment _env;
        public HomeController(AppDbContexto context, IWebHostEnvironment env,ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
            this._env = env;
        }

        public async Task<IActionResult> Index()
        {
            return _context.jugadores != null ?
                        View(await _context.jugadores.ToListAsync()) :
                        Problem("Entity set 'AppDbContexto.jugadores'  is null.");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}