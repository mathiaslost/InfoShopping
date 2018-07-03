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
    public class LojasController : Controller
    {
        private readonly Services.IGenericRepository<LojaModel> _repositoryLoja;
        private readonly Services.IGenericRepository<ShoppingModel> _repositoryShopping;
        private readonly UserManager<ApplicationUser> _userManager;

        public LojasController(
            Services.IGenericRepository<LojaModel> repository,
            Services.IGenericRepository<ShoppingModel> repoShopping,
            UserManager<ApplicationUser> userManager)
        {
            _repositoryLoja = repository;
            _repositoryShopping = repoShopping;
            _userManager = userManager;
        }

        // GET: CidadeModels
        public async Task<IActionResult> Index(int? id)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
                return Challenge();

            var applicationDbContext = await _repositoryLoja
                .GetAllAsync(c => c.OwnerId == currentUser.Id && (id == null || c.ShoppingId == id), c => c.Shopping);
            return View(applicationDbContext);
        }

        // GET: CidadeModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lojaModel = await _repositoryLoja.GetAsync(id.Value);
            ViewData["ShoppingId"] = new SelectList(_repositoryShopping.GetAll(), "ShoppingId", "Nome");

            if (lojaModel == null)
            {
                return NotFound();
            }

            return View(lojaModel);
        }

        // GET: CidadeModels/Create
        public IActionResult Create()
        {
            ViewData["ShoppingId"] = new SelectList(_repositoryShopping.GetAll(), "ShoppingId", "Nome");
            return View();
        }

        // POST: CidadeModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LojaId,Nome,cnpj,NomeFantasia,ShoppingId")] LojaModel lojaModel)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                if (currentUser == null)
                    return Unauthorized();

                lojaModel.OwnerId = currentUser.Id;

                await _repositoryLoja.InsertAsync(lojaModel);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShoppingId"] = new SelectList(await _repositoryLoja.GetAllAsync(), "ShoppingId", "Nome", lojaModel.ShoppingId);
            return View(lojaModel);
        }

        // GET: CidadeModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var lojaModel = await _repositoryLoja.GetAsync(id.Value);

            if (lojaModel == null)
                return NotFound();

            var list = await _repositoryShopping.GetAllAsync();

            ViewData["ShoppingId"] = new SelectList(await _repositoryLoja.GetAllAsync(), "ShoppingId", "Nome", lojaModel.ShoppingId);
            return View(lojaModel);
        }

        // POST: CidadeModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LojaId,Nome,cnpj,NomeFantasia,ShoppingId")] LojaModel lojaModel)
        {
            if (id != lojaModel.LojaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var currentUser = await _userManager.GetUserAsync(User);

                if (currentUser == null)
                    return Unauthorized();

                lojaModel.OwnerId = currentUser.Id;

                await _repositoryLoja.UpdateAsync(id, lojaModel);

                return RedirectToAction(nameof(Index));
            }
            ViewData["ShoppingId"] = new SelectList(await _repositoryLoja.GetAllAsync(), "ShoppingId", "Nome", lojaModel.ShoppingId);
            return View(lojaModel);
        }

        // GET: CidadeModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lojaModel = await _repositoryLoja.GetAsync(id);
            if (lojaModel == null)
            {
                return NotFound();
            }

            return View(lojaModel);
        }

        // POST: CidadeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repositoryLoja.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
