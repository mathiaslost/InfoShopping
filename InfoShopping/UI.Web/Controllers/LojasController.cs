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
    public class LojasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LojasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LojaModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LojaModel.Include(l => l.Endereco);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LojaModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lojaModel = await _context.LojaModel
                .Include(l => l.Endereco)
                .SingleOrDefaultAsync(m => m.LojaId == id);
            if (lojaModel == null)
            {
                return NotFound();
            }

            return View(lojaModel);
        }

        // GET: LojaModels/Create
        public IActionResult Create()
        {
            ViewData["EnderecoId"] = new SelectList(_context.Set<EnderecoModel>(), "EnderecoId", "EnderecoId");
            return View();
        }

        // POST: LojaModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LojaId,Nome,cnpj,NomeFantasia,EnderecoId")] LojaModel lojaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lojaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnderecoId"] = new SelectList(_context.Set<EnderecoModel>(), "EnderecoId", "EnderecoId", lojaModel.EnderecoId);
            return View(lojaModel);
        }

        // GET: LojaModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lojaModel = await _context.LojaModel.SingleOrDefaultAsync(m => m.LojaId == id);
            if (lojaModel == null)
            {
                return NotFound();
            }
            ViewData["EnderecoId"] = new SelectList(_context.Set<EnderecoModel>(), "EnderecoId", "EnderecoId", lojaModel.EnderecoId);
            return View(lojaModel);
        }

        // POST: LojaModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LojaId,Nome,cnpj,NomeFantasia,EnderecoId")] LojaModel lojaModel)
        {
            if (id != lojaModel.LojaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lojaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LojaModelExists(lojaModel.LojaId))
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
            ViewData["EnderecoId"] = new SelectList(_context.Set<EnderecoModel>(), "EnderecoId", "EnderecoId", lojaModel.EnderecoId);
            return View(lojaModel);
        }

        // GET: LojaModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lojaModel = await _context.LojaModel
                .Include(l => l.Endereco)
                .SingleOrDefaultAsync(m => m.LojaId == id);
            if (lojaModel == null)
            {
                return NotFound();
            }

            return View(lojaModel);
        }

        // POST: LojaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lojaModel = await _context.LojaModel.SingleOrDefaultAsync(m => m.LojaId == id);
            _context.LojaModel.Remove(lojaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LojaModelExists(int id)
        {
            return _context.LojaModel.Any(e => e.LojaId == id);
        }
    }
}
