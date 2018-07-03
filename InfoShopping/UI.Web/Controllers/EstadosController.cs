using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UI.Web.Data;
using UI.Web.Models;

namespace UI.Web.Controllers
{
    [Authorize]
    public class EstadosController : Controller
    {
        private readonly Services.IGenericRepository<EstadoModel> _repositoryEstado;

        public EstadosController(Services.IGenericRepository<EstadoModel> repoEstado)
        {
            _repositoryEstado = repoEstado;
        }

        // GET: EstadoModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await _repositoryEstado.GetAllAsync();
            return View(applicationDbContext);
        }

        // GET: EstadoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoModel = await _repositoryEstado.GetAsync(id);
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
                await _repositoryEstado.InsertAsync(estadoModel);
                return RedirectToAction(nameof(Index));
            }
            return View(estadoModel);
        }

        // GET: EstadoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var estadoModel = await _repositoryEstado.GetAsync(id.Value);

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
                await _repositoryEstado.UpdateAsync(id, estadoModel);

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

            var estadoModel = await _repositoryEstado.GetAsync(id);
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
            await _repositoryEstado.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
