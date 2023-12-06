using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication26.Models;

namespace WebApplication26.Controllers
{
    public class TrabajadoresController : Controller
    {
        private readonly TrabajadoresPruebaContext _context;

        public TrabajadoresController(TrabajadoresPruebaContext context)
        {
            _context = context;
        }

        // GET: Trabajadores
        public async Task<IActionResult> Index()
        {
            var trabajadoresPruebaContext = _context.Trabajadores
                .Include(t => t.IdDepartamentoNavigation)
                .Include(t => t.IdDistritoNavigation)
                .Include(t => t.IdProvinciaNavigation);

            return View(await trabajadoresPruebaContext.ToListAsync());
        }

        // GET: Trabajadores/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Trabajadores == null)
            {
                return NotFound();
            }

            var trabajadore = await _context.Trabajadores
                .Include(t => t.IdDepartamentoNavigation)
                .Include(t => t.IdDistritoNavigation)
                .Include(t => t.IdProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajadore == null)
            {
                return NotFound();
            }

            return View(trabajadore);
        }

        // GET: Trabajadores/Create
        public IActionResult Create()
        {

            ViewBag.IdDepartamento = new SelectList(_context.Departamentos, "Id", "NombreDepartamento");
            ViewBag.IdProvincia = new SelectList(_context.Provincia, "Id", "NombreProvincia");
            ViewBag.IdDistrito = new SelectList(_context.Distritos, "Id", "NombreDistrito");
            return View();
        }

        // POST: Trabajadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajadore trabajadore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trabajadore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.IdDepartamento = new SelectList(_context.Departamentos, "Id", "NombreDepartamento", trabajadore.IdDepartamento);
            ViewBag.IdProvincia = new SelectList(_context.Provincia, "Id", "NombreProvincia", trabajadore.IdProvincia);
            ViewBag.IdDistrito = new SelectList(_context.Distritos, "Id", "NombreDistrito", trabajadore.IdDistrito);
            return View(trabajadore);
        }

        // GET: Trabajadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Trabajadores == null)
            {
                return NotFound();
            }

            var trabajadore = await _context.Trabajadores.FindAsync(id);
            if (trabajadore == null)
            {
                return NotFound();
            }

            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento", trabajadore.IdDepartamento);
            ViewData["IdDistrito"] = new SelectList(_context.Distritos, "Id", "NombreDistrito", trabajadore.IdDistrito);
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "Id", "NombreProvincia", trabajadore.IdProvincia);

            return View(trabajadore);
        }

        // POST: Trabajadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajadore trabajadore)
        {
            if (id != trabajadore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var existenTrabajadore = await _context.Trabajadores
                        .Include(t => t.IdDepartamentoNavigation)
                        .Include(t => t.IdProvinciaNavigation)
                        .Include(t => t.IdDistritoNavigation)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    if (existenTrabajadore == null)
                    {
                        return NotFound();
                    }

                    // Actualizar las propiedades del objeto cargado con las propiedades del objeto editado
                    existenTrabajadore.TipoDocumento = trabajadore.TipoDocumento;
                    existenTrabajadore.NumeroDocumento = trabajadore.NumeroDocumento;
                    existenTrabajadore.Nombres = trabajadore.Nombres;
                    existenTrabajadore.Sexo = trabajadore.Sexo;
                    existenTrabajadore.IdDepartamento = trabajadore.IdDepartamento;
                    existenTrabajadore.IdProvincia = trabajadore.IdProvincia;
                    existenTrabajadore.IdDistrito = trabajadore.IdDistrito;

                    // Actualizar y guardar los cambios
                    _context.Update(existenTrabajadore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrabajadoreExists(trabajadore.Id))
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

            // Cargar las listas desplegables con los nombres correctos
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento", trabajadore.IdDepartamento);
            ViewData["IdDistrito"] = new SelectList(_context.Distritos, "Id", "NombreDistrito", trabajadore.IdDistrito);
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "Id", "NombreProvincia", trabajadore.IdProvincia);

            return View(trabajadore);
        }

        // GET: Trabajadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Trabajadores == null)
            {
                return NotFound();
            }

            var trabajadore = await _context.Trabajadores
                .Include(t => t.IdDepartamentoNavigation)
                .Include(t => t.IdDistritoNavigation)
                .Include(t => t.IdProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajadore == null)
            {
                return NotFound();
            }

            return View(trabajadore);
        }

        // POST: Trabajadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Trabajadores == null)
            {
                return Problem("Entity set 'TrabajadoresPruebaContext.Trabajadores'  is null.");
            }
            var trabajadore = await _context.Trabajadores.FindAsync(id);
            if (trabajadore != null)
            {
                _context.Trabajadores.Remove(trabajadore);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrabajadoreExists(int id)
        {
          return (_context.Trabajadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public IActionResult GetProvincias(int idDepartamento)
        {
            var provincias = _context.Provincia.Where(p => p.IdDepartamento == idDepartamento).ToList();
            return Json(provincias);
        }

        public IActionResult GetDistritos(int idProvincia)
        {
            var distritos = _context.Distritos.Where(d => d.IdProvincia == idProvincia).ToList();
            return Json(distritos);
        }
    }
}

