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
    public class EnderecoModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnderecoModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EnderecoModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EnderecoModel.Include(e => e.Cidade);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EnderecoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enderecoModel = await _context.EnderecoModel
                .Include(e => e.Cidade)
                .SingleOrDefaultAsync(m => m.EnderecoId == id);
            if (enderecoModel == null)
            {
                return NotFound();
            }

            return View(enderecoModel);
        }

        // GET: EnderecoModels/Create
        public IActionResult Create()
        {
            ViewData["CidadeId"] = new SelectList(_context.CidadeModel, "CidadeId", "Nome");
            return View();
        }

        // POST: EnderecoModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnderecoId,Rua,Numero,Bairro,Complemento,Cep,CidadeId")] EnderecoModel enderecoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enderecoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CidadeId"] = new SelectList(_context.CidadeModel, "CidadeId", "Nome", enderecoModel.CidadeId);
            return View(enderecoModel);
        }

        // GET: EnderecoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enderecoModel = await _context.EnderecoModel.SingleOrDefaultAsync(m => m.EnderecoId == id);
            if (enderecoModel == null)
            {
                return NotFound();
            }
            ViewData["CidadeId"] = new SelectList(_context.CidadeModel, "CidadeId", "Nome", enderecoModel.CidadeId);
            return View(enderecoModel);
        }

        // POST: EnderecoModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnderecoId,Rua,Numero,Bairro,Complemento,Cep,CidadeId")] EnderecoModel enderecoModel)
        {
            if (id != enderecoModel.EnderecoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enderecoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnderecoModelExists(enderecoModel.EnderecoId))
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
            ViewData["CidadeId"] = new SelectList(_context.CidadeModel, "CidadeId", "Nome", enderecoModel.CidadeId);
            return View(enderecoModel);
        }

        // GET: EnderecoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enderecoModel = await _context.EnderecoModel
                .Include(e => e.Cidade)
                .SingleOrDefaultAsync(m => m.EnderecoId == id);
            if (enderecoModel == null)
            {
                return NotFound();
            }

            return View(enderecoModel);
        }

        // POST: EnderecoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enderecoModel = await _context.EnderecoModel.SingleOrDefaultAsync(m => m.EnderecoId == id);
            _context.EnderecoModel.Remove(enderecoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnderecoModelExists(int id)
        {
            return _context.EnderecoModel.Any(e => e.EnderecoId == id);
        }
    }
}
