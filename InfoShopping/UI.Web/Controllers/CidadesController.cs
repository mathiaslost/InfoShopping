using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UI.Web.Data;
using UI.Web.Models;

namespace UI.Web.Controllers
{
    [Authorize]
    public class CidadesController : Controller
    {
        private readonly Services.IGenericRepository<CidadeModel> _repositoryCidade;
        private readonly Services.IGenericRepository<EstadoModel> _repositoryEstado;
        private readonly UserManager<ApplicationUser> _userManager;

        public CidadesController(
            Services.IGenericRepository<CidadeModel> repository,
            Services.IGenericRepository<EstadoModel> repoEstado,
            UserManager<ApplicationUser> userManager)
        {
            _repositoryCidade = repository;
            _repositoryEstado = repoEstado;
            _userManager = userManager;
        }

        // GET: CidadeModels
        public async Task<IActionResult> Index(int? id)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
                return Challenge();

            var isAdm = await _userManager.IsInRoleAsync(currentUser, Constants.AdministratorRole);

            var applicationDbContext = await _repositoryCidade
                .GetAllAsync(c => (isAdm || c.OwnerId == currentUser.Id)
                && (id == null || c.EstadoId == id), c => c.Estado);
            return View(applicationDbContext);
        }

        // GET: CidadeModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidadeModel = await _repositoryCidade.GetAsync(id.Value);
            ViewData["EstadoId"] = new SelectList(_repositoryEstado.GetAll(), "EstadoId", "Nome");

            if (cidadeModel == null)
            {
                return NotFound();
            }

            return View(cidadeModel);
        }

        // GET: CidadeModels/Create
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(_repositoryEstado.GetAll(), "EstadoId", "Nome");
            return View();
        }

        // POST: CidadeModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CidadeId,Nome,EstadoId")] CidadeModel cidadeModel)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                if (currentUser == null)
                    return Unauthorized();

                cidadeModel.OwnerId = currentUser.Id;

                await _repositoryCidade.InsertAsync(cidadeModel);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoId"] = new SelectList(await _repositoryCidade.GetAllAsync(), "EstadoId", "Nome", cidadeModel.EstadoId);
            return View(cidadeModel);
        }

        // GET: CidadeModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var cidadeModel = await _repositoryCidade.GetAsync(id.Value);

            if (cidadeModel == null)
                return NotFound();

            var list = await _repositoryEstado.GetAllAsync();

            ViewData["EstadoId"] = new SelectList(list, "EstadoId", "Nome", cidadeModel.EstadoId);
            return View(cidadeModel);
        }

        // POST: CidadeModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CidadeId,Nome,EstadoId")] CidadeModel cidadeModel)
        {
            if (id != cidadeModel.CidadeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _repositoryCidade.UpdateAsync(id, cidadeModel);

                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoId"] = new SelectList(_repositoryEstado.GetAll(), "EstadoId", "Nome");
            return View(cidadeModel);
        }

        // GET: CidadeModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidadeModel = await _repositoryCidade.GetAsync(id);
            if (cidadeModel == null)
            {
                return NotFound();
            }

            return View(cidadeModel);
        }

        // POST: CidadeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repositoryCidade.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
