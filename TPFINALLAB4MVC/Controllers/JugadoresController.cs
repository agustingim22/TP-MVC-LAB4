using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using TPFINALLAB4MVC.Models;
using TPFINALLAB4MVC.ViewsModels;

namespace TPFINALLAB4MVC.Controllers
{
    [Authorize]
    public class JugadoresController : Controller
    {
        private readonly AppDbContexto _context;
        private readonly IWebHostEnvironment _env;

        public JugadoresController(AppDbContexto context, IWebHostEnvironment env)
        {
            _context = context;
            this._env = env;
        }

        // GET: Jugadores
        [AllowAnonymous]
        public async Task<IActionResult> Index(string busquedaNombre, int? busquedaEdad, string busquedaNickName, int page = 1, int pageSize = 3)
        {
            var appDBcontexto = _context.jugadores.Select(a => a);

            if (!string.IsNullOrEmpty(busquedaNombre))
            {
                appDBcontexto = appDBcontexto.Where(a => a.nombre.Contains(busquedaNombre));
            }

            if (!string.IsNullOrEmpty(busquedaNickName))
            {
                appDBcontexto = appDBcontexto.Where(a => a.nickName.Contains(busquedaNickName));
            }

            if (busquedaEdad.HasValue)
            {
                appDBcontexto = appDBcontexto.Where(a => a.edad == busquedaEdad.Value);
            }

            // Calcula la cantidad total de elementos y realiza la paginación
            var totalItems = await appDBcontexto.CountAsync();
            var itemsToDisplay = await appDBcontexto.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            JugadorViewModel modelo = new JugadorViewModel()
            {
                jugadores = itemsToDisplay,
                nombre = busquedaNombre,
                edad = busquedaEdad,
                nickName = busquedaNickName,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            return View(modelo);
        }


        public async Task<IActionResult> Importar()
        {
            var archivos = HttpContext.Request.Form.Files;
            if (archivos != null && archivos.Count > 0)
            {
                var archivo = archivos[0];
                if (archivo.Length > 0)
                {
                    var pathDestino = Path.Combine(_env.WebRootPath, "importaciones");
                    var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivo.FileName);
                    string rutaCompleta = Path.Combine(pathDestino, archivoDestino);

                    using (var filestream = new FileStream(rutaCompleta, FileMode.Create))
                    {
                        archivo.CopyTo(filestream);
                    }

                    using (var package = new ExcelPackage(new FileInfo(rutaCompleta)))
                    {
                        var hojaExcel = package.Workbook.Worksheets[0];
                        List<Jugador> JugadoresArch = new List<Jugador>();

                        int cantFilas = hojaExcel.Dimension.Rows;

                        for (int fila = 2; fila <= cantFilas; fila++)
                        {
                            int salida;
                            Jugador jugador = new Jugador();
                            jugador.nombre = hojaExcel.Cells[fila, 1].Value?.ToString();
                            jugador.apellido = hojaExcel.Cells[fila, 2].Value?.ToString();
                            jugador.nickName = hojaExcel.Cells[fila, 3].Value?.ToString();
                            jugador.edad = int.TryParse(hojaExcel.Cells[fila, 4].Value?.ToString(), out salida) ? salida : 0;
                            
                            JugadoresArch.Add(jugador);
                        }


                        if (JugadoresArch.Count > 0)
                        {
                            _context.jugadores.AddRange(JugadoresArch);
                            _context.SaveChanges();

                            ViewBag.resultado = "Se subió el archivo exitosamente";
                        }
                        else
                            ViewBag.resultado = "Error en el formato de archivo";
                    }

                    if (System.IO.File.Exists(rutaCompleta))
                        System.IO.File.Delete(rutaCompleta);
                }
                else
                    ViewBag.resultado = "Error en el archivo vacío";
            }
            else
                ViewBag.resultado = "Error en el archivo enviado";

            return View("Index", await _context.jugadores.ToListAsync());
        }

        // GET: Jugadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.jugadores == null)
            {
                return NotFound();
            }

            var jugador = await _context.jugadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jugador == null)
            {
                return NotFound();
            }

            return View(jugador);
        }

        // GET: Jugadores/Create
        public IActionResult Create()
        {
            ViewData["PartidoId"] = new SelectList(_context.partidos, "Id", "nickName");
            return View();
        }

        // POST: Jugadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nombre,apellido,nickName,edad")] Jugador modelJugador)
        {
            if (ModelState.IsValid)
            {
                modelJugador.fotografia = cargarFoto("");

                _context.Add(modelJugador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(modelJugador);
        }

        // GET: Jugadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.jugadores == null)
            {
                return NotFound();
            }

            var jugador = await _context.jugadores.FindAsync(id);
            if (jugador == null)
            {
                return NotFound();
            }
            return View(jugador);
        }

        // POST: Jugadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nombre,apellido,nickName,edad")] Jugador jugador)
        {
            if (id != jugador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string nuevaFoto =
                       cargarFoto(string.IsNullOrEmpty(jugador.fotografia) ? "" : jugador.fotografia);

                    if (!string.IsNullOrEmpty(nuevaFoto))
                        jugador.fotografia = nuevaFoto;

                    _context.Update(jugador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JugadorExists(jugador.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(jugador);
        }

        // GET: Jugadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.jugadores == null)
            {
                return NotFound();
            }

            var jugador = await _context.jugadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jugador == null)
            {
                return NotFound();
            }

            return View(jugador);
        }

        // POST: Jugadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.jugadores == null)
            {
                return Problem("Entity set 'AppDbContexto.jugadores'  is null.");
            }
            var jugador = await _context.jugadores.FindAsync(id);
            if (jugador != null)
            {
                _context.jugadores.Remove(jugador);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JugadorExists(int id)
        {
          return (_context.jugadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private string cargarFoto(string fotoAnterior)
        {
            var archivos = HttpContext.Request.Form.Files;
            if (archivos != null && archivos.Count > 0)
            {
                var archivoFoto = archivos[0];
                if (archivoFoto.Length > 0)
                {
                    var pathDestino = Path.Combine(_env.WebRootPath, "images\\jugadores");
                    fotoAnterior = Path.Combine(pathDestino, fotoAnterior);
                    if (System.IO.File.Exists(fotoAnterior))
                        System.IO.File.Delete(fotoAnterior);

                    var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);

                    using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                    {
                        archivoFoto.CopyTo(filestream);
                        return archivoDestino;
                    };
                }
            }
            return "";
        }
    }
}
