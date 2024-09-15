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
    public class LigaController : Controller
    {
        private readonly DbfutbolContext _context;

        public LigaController(DbfutbolContext context)
        {
            _context = context;
        }

        // GET: Liga
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ligas.ToListAsync());
        }

        // GET: Liga/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var liga = await _context.Ligas
                .FirstOrDefaultAsync(m => m.IdLiga == id);
            if (liga == null)
            {
                return NotFound();
            }

            return View(liga);
        }

        // GET: Liga/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Liga/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLiga,NombreLiga,PaisLiga")] Liga liga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(liga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(liga);
        }

        // GET: Liga/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var liga = await _context.Ligas.FindAsync(id);
            if (liga == null)
            {
                return NotFound();
            }
            return View(liga);
        }

        // POST: Liga/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLiga,NombreLiga,PaisLiga")] Liga liga)
        {
            if (id != liga.IdLiga)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(liga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LigaExists(liga.IdLiga))
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
            return View(liga);
        }

        // GET: Liga/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var liga = await _context.Ligas
                .FirstOrDefaultAsync(m => m.IdLiga == id);
            if (liga == null)
            {
                return NotFound();
            }

            return View(liga);
        }

        // POST: Liga/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var liga = await _context.Ligas.FindAsync(id);
            if (liga != null)
            {
                _context.Ligas.Remove(liga);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LigaExists(int id)
        {
            return _context.Ligas.Any(e => e.IdLiga == id);
        }
    }
}
