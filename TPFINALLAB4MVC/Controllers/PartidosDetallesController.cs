using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPFINALLAB4MVC.Models;

namespace TPFINALLAB4MVC.Controllers
{
    [Authorize]
    public class PartidosDetallesController : Controller
    {
        private readonly AppDbContexto _context;
       

        public PartidosDetallesController(AppDbContexto context)
        {
            _context = context;
        }

        // GET: PartidosDetalles
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
              return _context.partidosDetalles != null ? 
                          View(await _context.partidosDetalles.ToListAsync()) :
                          Problem("Entity set 'AppDbContexto.partidosDetalles'  is null.");
        }

        // GET: PartidosDetalles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.partidosDetalles == null)
            {
                return NotFound();
            }

            var partidoDetalle = await _context.partidosDetalles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partidoDetalle == null)
            {
                return NotFound();
            }

            return View(partidoDetalle);
        }

        // GET: PartidosDetalles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PartidosDetalles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,golesJugador,golesRival,cantRojas,cantAmarillas,IdPartido")] PartidoDetalle modelPartidoDetalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modelPartidoDetalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modelPartidoDetalle);
        }

        // GET: PartidosDetalles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.partidosDetalles == null)
            {
                return NotFound();
            }

            var partidoDetalle = await _context.partidosDetalles.FindAsync(id);
            if (partidoDetalle == null)
            {
                return NotFound();
            }
            return View(partidoDetalle);
        }

        // POST: PartidosDetalles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,golesJugador,golesRival,cantRojas,cantAmarillas,IdPartido")] PartidoDetalle partidoDetalle)
        {
            if (id != partidoDetalle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partidoDetalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartidoDetalleExists(partidoDetalle.Id))
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
            return View(partidoDetalle);
        }

        // GET: PartidosDetalles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.partidosDetalles == null)
            {
                return NotFound();
            }

            var partidoDetalle = await _context.partidosDetalles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partidoDetalle == null)
            {
                return NotFound();
            }

            return View(partidoDetalle);
        }

        // POST: PartidosDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.partidosDetalles == null)
            {
                return Problem("Entity set 'AppDbContexto.partidosDetalles'  is null.");
            }
            var partidoDetalle = await _context.partidosDetalles.FindAsync(id);
            if (partidoDetalle != null)
            {
                _context.partidosDetalles.Remove(partidoDetalle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartidoDetalleExists(int id)
        {
          return (_context.partidosDetalles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
