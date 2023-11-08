using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPFINALLAB4MVC.Models;
using TPFINALLAB4MVC.ViewsModels;

namespace TPFINALLAB4MVC.Controllers
{
    [Authorize]
    public class PartidosController : Controller
    {
        private readonly AppDbContexto _context;

        public PartidosController(AppDbContexto context)
        {
            _context = context;
        }

        // GET: Partidos
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? busquedaFecha, string busquedaNickNameRival, int page = 1, int pageSize = 3) {
            var appDBcontexto = _context.partidos.Include(x => x.estado).Include(x => x.jugador).Select(a => a);
            if (!string.IsNullOrEmpty(busquedaNickNameRival))
            {
                appDBcontexto = appDBcontexto.Where(a => a.NickNameRival.Contains(busquedaNickNameRival));
            }
            if (busquedaFecha.HasValue)
            {
                appDBcontexto = appDBcontexto.Where(a => a.fecha == busquedaFecha.ToString());
            }

            var totalItems = await appDBcontexto.CountAsync();
            var itemsToDisplay = await appDBcontexto.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PartidosViewModel modelo = new PartidosViewModel()
            {
                partidos = itemsToDisplay,
                NickNameRival = busquedaNickNameRival,
                fecha = busquedaFecha,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };
            return View(modelo);
        }

        // GET: Partidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.partidos == null)
            {
                return NotFound();
            }

            var partido = await _context.partidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partido == null)
            {
                return NotFound();
            }

            return View(partido);
        }

        // GET: Partidos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Partidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,fecha,NickNameRival,IdJugador,IdEstado")] Partido modelPartido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modelPartido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modelPartido);
        }

        // GET: Partidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.partidos == null)
            {
                return NotFound();
            }

            var partido = await _context.partidos.FindAsync(id);
            if (partido == null)
            {
                return NotFound();
            }
            return View(partido);
        }

        // POST: Partidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,fecha,NickNameRival,IdJugador,IdEstado")] Partido partido)
        {
            if (id != partido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartidoExists(partido.Id))
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
            return View(partido);
        }

        // GET: Partidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.partidos == null)
            {
                return NotFound();
            }

            var partido = await _context.partidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partido == null)
            {
                return NotFound();
            }

            return View(partido);
        }

        // POST: Partidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.partidos == null)
            {
                return Problem("Entity set 'AppDbContexto.partidos'  is null.");
            }
            var partido = await _context.partidos.FindAsync(id);
            if (partido != null)
            {
                _context.partidos.Remove(partido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartidoExists(int id)
        {
          return (_context.partidos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
