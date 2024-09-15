using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApiFutbol.Models;

namespace ApiFutbol.Controllers
{
    public class PosicionController : Controller
    {
        private readonly DbfutbolContext _context;

        public PosicionController(DbfutbolContext context)
        {
            _context = context;
        }

        // GET: Posicion
        public async Task<IActionResult> Index()
        {
            return View(await _context.Posicions.ToListAsync());
        }

        // GET: Posicion/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posicion = await _context.Posicions
                .FirstOrDefaultAsync(m => m.IdPosicion == id);
            if (posicion == null)
            {
                return NotFound();
            }

            return View(posicion);
        }

        // GET: Posicion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posicion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPosicion,NombrePosicion")] Posicion posicion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(posicion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(posicion);
        }

        // GET: Posicion/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posicion = await _context.Posicions.FindAsync(id);
            if (posicion == null)
            {
                return NotFound();
            }
            return View(posicion);
        }

        // POST: Posicion/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPosicion,NombrePosicion")] Posicion posicion)
        {
            if (id != posicion.IdPosicion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posicion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PosicionExists(posicion.IdPosicion))
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
            return View(posicion);
        }

        // GET: Posicion/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posicion = await _context.Posicions
                .FirstOrDefaultAsync(m => m.IdPosicion == id);
            if (posicion == null)
            {
                return NotFound();
            }

            return View(posicion);
        }

        // POST: Posicion/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var posicion = await _context.Posicions.FindAsync(id);
            if (posicion != null)
            {
                _context.Posicions.Remove(posicion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PosicionExists(int id)
        {
            return _context.Posicions.Any(e => e.IdPosicion == id);
        }
    }
}
