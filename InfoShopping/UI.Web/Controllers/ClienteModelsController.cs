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
    public class ClienteModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClienteModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClienteModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClienteModel.Include(c => c.Endereco);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClienteModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteModel = await _context.ClienteModel
                .Include(c => c.Endereco)
                .SingleOrDefaultAsync(m => m.ClienteId == id);
            if (clienteModel == null)
            {
                return NotFound();
            }

            return View(clienteModel);
        }

        // GET: ClienteModels/Create
        public IActionResult Create()
        {
            ViewData["EnderecoId"] = new SelectList(_context.Set<EnderecoModel>(), "EnderecoId", "EnderecoId");
            return View();
        }

        // POST: ClienteModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,Nome,Cpf,EnderecoId")] ClienteModel clienteModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnderecoId"] = new SelectList(_context.Set<EnderecoModel>(), "EnderecoId", "EnderecoId", clienteModel.EnderecoId);
            return View(clienteModel);
        }

        // GET: ClienteModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteModel = await _context.ClienteModel.SingleOrDefaultAsync(m => m.ClienteId == id);
            if (clienteModel == null)
            {
                return NotFound();
            }
            ViewData["EnderecoId"] = new SelectList(_context.Set<EnderecoModel>(), "EnderecoId", "EnderecoId", clienteModel.EnderecoId);
            return View(clienteModel);
        }

        // POST: ClienteModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteId,Nome,Cpf,EnderecoId")] ClienteModel clienteModel)
        {
            if (id != clienteModel.ClienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteModelExists(clienteModel.ClienteId))
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
            ViewData["EnderecoId"] = new SelectList(_context.Set<EnderecoModel>(), "EnderecoId", "EnderecoId", clienteModel.EnderecoId);
            return View(clienteModel);
        }

        // GET: ClienteModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteModel = await _context.ClienteModel
                .Include(c => c.Endereco)
                .SingleOrDefaultAsync(m => m.ClienteId == id);
            if (clienteModel == null)
            {
                return NotFound();
            }

            return View(clienteModel);
        }

        // POST: ClienteModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clienteModel = await _context.ClienteModel.SingleOrDefaultAsync(m => m.ClienteId == id);
            _context.ClienteModel.Remove(clienteModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteModelExists(int id)
        {
            return _context.ClienteModel.Any(e => e.ClienteId == id);
        }
    }
}
