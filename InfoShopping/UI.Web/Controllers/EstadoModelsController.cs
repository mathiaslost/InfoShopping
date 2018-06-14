using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UI.Web.Data;
using UI.Web.Models;

namespace UI.Web.Controllers
{
    public class EstadoModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstadoModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EstadoModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstadoModel.ToListAsync());
        }

        // GET: EstadoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoModel = await _context.EstadoModel
                .SingleOrDefaultAsync(m => m.EstadoId == id);
            if (estadoModel == null)
            {
                return NotFound();
            }

            return View(estadoModel);
        }

        // GET: EstadoModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstadoModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstadoId,Nome")] EstadoModel estadoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadoModel);
        }

        // GET: EstadoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoModel = await _context.EstadoModel.SingleOrDefaultAsync(m => m.EstadoId == id);
            if (estadoModel == null)
            {
                return NotFound();
            }
            return View(estadoModel);
        }

        // POST: EstadoModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstadoId,Nome")] EstadoModel estadoModel)
        {
            if (id != estadoModel.EstadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoModelExists(estadoModel.EstadoId))
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
            return View(estadoModel);
        }

        // GET: EstadoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoModel = await _context.EstadoModel
                .SingleOrDefaultAsync(m => m.EstadoId == id);
            if (estadoModel == null)
            {
                return NotFound();
            }

            return View(estadoModel);
        }

        // POST: EstadoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadoModel = await _context.EstadoModel.SingleOrDefaultAsync(m => m.EstadoId == id);
            _context.EstadoModel.Remove(estadoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoModelExists(int id)
        {
            return _context.EstadoModel.Any(e => e.EstadoId == id);
        }
    }
}
