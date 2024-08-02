using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApiFutbol.Models;
using Microsoft.Data.SqlClient;

namespace ApiFutbol.Controllers
{
    public class JugadorController : Controller
    {
        private readonly DbfutbolContext _context;

        public JugadorController(DbfutbolContext context)
        {
            _context = context;
        }

        // GET: Jugador
        public async Task<IActionResult> Index(string nombre, string pais, string equipo, string posicion, string sortOrder, string sortDirection)
        {
            var dbfutbolContext = _context.Jugador
                .Include(j => j.IdEquipoNavigation)
                .Include(j => j.IdPosicionNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
            {
                dbfutbolContext = dbfutbolContext.Where(j => j.NombreJugador.Contains(nombre));
            }

            if (!string.IsNullOrEmpty(pais))
            {
                dbfutbolContext = dbfutbolContext.Where(j => j.PaisJugador.Contains(pais));
            }

            if (!string.IsNullOrEmpty(equipo))
            {
                dbfutbolContext = dbfutbolContext.Where(j => j.IdEquipoNavigation.NombreEquipo.Contains(equipo));
            }

            if (!string.IsNullOrEmpty(posicion))
            {
                dbfutbolContext = dbfutbolContext.Where(j => j.IdPosicionNavigation.NombrePosicion.Contains(posicion));
            }

            // Aplicar órden a la tabla
            switch (sortOrder)
            {
                case "Nombre":
                    dbfutbolContext = sortDirection == "asc" ? dbfutbolContext.OrderBy(j => j.NombreJugador) : dbfutbolContext.OrderByDescending(j => j.NombreJugador);
                    break;
                case "Pais":
                    dbfutbolContext = sortDirection == "asc" ? dbfutbolContext.OrderBy(j => j.PaisJugador) : dbfutbolContext.OrderByDescending(j => j.PaisJugador);
                    break;
                case "FechaNacimiento":
                    dbfutbolContext = sortDirection == "asc" ? dbfutbolContext.OrderBy(j => j.FechaNacimiento) : dbfutbolContext.OrderByDescending(j => j.FechaNacimiento);
                    break;
                case "Equipo":
                    dbfutbolContext = sortDirection == "asc" ? dbfutbolContext.OrderBy(j => j.IdEquipoNavigation.NombreEquipo) : dbfutbolContext.OrderByDescending(j => j.IdEquipoNavigation.NombreEquipo);
                    break;
                case "Posicion":
                    dbfutbolContext = sortDirection == "asc" ? dbfutbolContext.OrderBy(j => j.IdPosicionNavigation.NombrePosicion) : dbfutbolContext.OrderByDescending(j => j.IdPosicionNavigation.NombrePosicion);
                    break;
                default:
                    dbfutbolContext = dbfutbolContext.OrderBy(j => j.NombreJugador); // Default sorting
                    break;
            }

            // Fetch filtros
            var jugadores = await dbfutbolContext.ToListAsync();

            // Views filtros
            ViewData["Nombre"] = nombre;
            ViewData["Pais"] = pais;
            ViewData["Equipo"] = equipo;
            ViewData["Posicion"] = posicion;
            ViewData["SortOrder"] = sortOrder;
            ViewData["SortDirection"] = sortDirection;

            return View(jugadores);
        }

        // GET: Jugador/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugador = await _context.Jugador
                .Include(j => j.IdEquipoNavigation)
                .Include(j => j.IdPosicionNavigation)
                .FirstOrDefaultAsync(m => m.IdJugador == id);
            if (jugador == null)
            {
                return NotFound();
            }

            return View(jugador);
        }

        // GET: Jugador/Create
        public IActionResult Create()
        {
            ViewBag.IdEquipo = new SelectList(_context.Equipos, "IdEquipo", "NombreEquipo");
            ViewBag.IdPosicion = new SelectList(_context.Posicions, "IdPosicion", "NombrePosicion");
            return View();
        }

        // POST: Jugador/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreJugador,PaisJugador,IdPosicion,IdEquipo,FechaNacimiento,Imagen")] Jugador jugador)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(jugador);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Error al guardar en la base de datos: {ex.Message}");
                    ModelState.AddModelError("", "No se pudo guardar el jugador. Intenta nuevamente.");
                }
            } 
            else
            {
                ModelState.AddModelError("", "Los datos proporcionados no son válidos.");
            }
            ViewBag.IdEquipo = new SelectList(_context.Equipos, "IdEquipo", "NombreEquipo", jugador.IdEquipo);
            ViewBag.IdPosicion = new SelectList(_context.Posicions, "IdPosicion", "NombrePosicion", jugador.IdPosicion);
            return View(jugador);
        }

        // GET: Jugador/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugador = await _context.Jugador.FindAsync(id);
            if (jugador == null)
            {
                return NotFound();
            }
            ViewData["IdEquipo"] = new SelectList(_context.Equipos, "IdEquipo", "NombreEquipo", jugador.IdEquipo);
            ViewData["IdPosicion"] = new SelectList(_context.Posicions, "IdPosicion", "NombrePosicion", jugador.IdPosicion);
            return View(jugador);
        }

        // POST: Jugador/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdJugador, NombreJugador,PaisJugador,IdPosicion,IdEquipo,FechaNacimiento,Imagen")] Jugador jugador)
        {
            if (id != jugador.IdJugador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jugador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JugadorExists(jugador.IdJugador))
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
            ViewData["IdEquipo"] = new SelectList(_context.Equipos, "IdEquipo", "IdEquipo", jugador.IdEquipo);
            ViewData["IdPosicion"] = new SelectList(_context.Posicions, "IdPosicion", "IdPosicion", jugador.IdPosicion);
            return View(jugador);
        }

        // GET: Jugador/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugador = await _context.Jugador
                .Include(j => j.IdEquipoNavigation)
                .Include(j => j.IdPosicionNavigation)
                .FirstOrDefaultAsync(m => m.IdJugador == id);
            if (jugador == null)
            {
                return NotFound();
            }

            return View(jugador);
        }

        // POST: Jugador/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jugador = await _context.Jugador.FindAsync(id);
            if (jugador != null)
            {
                _context.Jugador.Remove(jugador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private bool JugadorExists(int id)
        {
            return _context.Jugador.Any(e => e.IdJugador == id);
        }

        // Acción para mostrar la vista Card
        public async Task<IActionResult> Card()
        {
            // Obtén todos los jugadores junto con sus relaciones
            var jugadores = await _context.Jugador
                .Include(j => j.IdEquipoNavigation)
                .Include(j => j.IdPosicionNavigation)
                .ToListAsync();

            // Retorna la vista con todos los jugadores
            return View(jugadores);
        }
    }
}
