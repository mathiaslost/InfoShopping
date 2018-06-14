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
    public class ShoppingModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ShoppingModel.Include(s => s.Endereco);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ShoppingModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingModel = await _context.ShoppingModel
                .Include(s => s.Endereco)
                .SingleOrDefaultAsync(m => m.ShoppingId == id);
            if (shoppingModel == null)
            {
                return NotFound();
            }

            return View(shoppingModel);
        }

        // GET: ShoppingModels/Create
        public IActionResult Create()
        {
            ViewData["EnderecoId"] = new SelectList(_context.Set<EnderecoModel>(), "EnderecoId", "EnderecoId");
            return View();
        }

        // POST: ShoppingModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShoppingId,Nome,CNPJ,Email,EnderecoId")] ShoppingModel shoppingModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnderecoId"] = new SelectList(_context.Set<EnderecoModel>(), "EnderecoId", "EnderecoId", shoppingModel.EnderecoId);
            return View(shoppingModel);
        }

        // GET: ShoppingModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingModel = await _context.ShoppingModel.SingleOrDefaultAsync(m => m.ShoppingId == id);
            if (shoppingModel == null)
            {
                return NotFound();
            }
            ViewData["EnderecoId"] = new SelectList(_context.Set<EnderecoModel>(), "EnderecoId", "EnderecoId", shoppingModel.EnderecoId);
            return View(shoppingModel);
        }

        // POST: ShoppingModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShoppingId,Nome,CNPJ,Email,EnderecoId")] ShoppingModel shoppingModel)
        {
            if (id != shoppingModel.ShoppingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingModelExists(shoppingModel.ShoppingId))
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
            ViewData["EnderecoId"] = new SelectList(_context.Set<EnderecoModel>(), "EnderecoId", "EnderecoId", shoppingModel.EnderecoId);
            return View(shoppingModel);
        }

        // GET: ShoppingModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingModel = await _context.ShoppingModel
                .Include(s => s.Endereco)
                .SingleOrDefaultAsync(m => m.ShoppingId == id);
            if (shoppingModel == null)
            {
                return NotFound();
            }

            return View(shoppingModel);
        }

        // POST: ShoppingModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppingModel = await _context.ShoppingModel.SingleOrDefaultAsync(m => m.ShoppingId == id);
            _context.ShoppingModel.Remove(shoppingModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingModelExists(int id)
        {
            return _context.ShoppingModel.Any(e => e.ShoppingId == id);
        }
    }
}
