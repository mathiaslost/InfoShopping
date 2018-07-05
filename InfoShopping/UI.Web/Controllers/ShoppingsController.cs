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
    public class ShoppingsController : Controller
    {
        private readonly Services.IGenericRepository<ShoppingModel> _repositoryShopping;
        private readonly Services.IGenericRepository<EnderecoModel> _repositoryEndereco;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingsController(
            Services.IGenericRepository<ShoppingModel> repository,
            Services.IGenericRepository<EnderecoModel> repoEndereco,
            UserManager<ApplicationUser> userManager)
        {
            _repositoryShopping = repository;
            _repositoryEndereco = repoEndereco;
            _userManager = userManager;
        }

        // GET: ShoppingModels
        public async Task<IActionResult> Index(int? id)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
                return Challenge();

            var applicationDbContext = await _repositoryShopping
                .GetAllAsync(c => c.OwnerId == currentUser.Id && (id == null || c.EnderecoId == id), c => c.Endereco);
            return View(applicationDbContext);
        }

        // GET: ShoppingModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingModel = await _repositoryShopping.GetAsync(id.Value);
            ViewData["EnderecoId"] = new SelectList(_repositoryEndereco.GetAll(), "EnderecoId", "Rua");

            if (shoppingModel == null)
            {
                return NotFound();
            }

            return View(shoppingModel);
        }

        // GET: CidadeModels/Create
        public IActionResult Create()
        {
            ViewData["EnderecoId"] = new SelectList(_repositoryEndereco.GetAll(), "EnderecoId", "Rua");
            return View();
        }

        // POST: CidadeModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShoppingId,Nome,CNPJ,Email,EnderecoId")] ShoppingModel shoppingModel)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                if (currentUser == null)
                    return Unauthorized();

                shoppingModel.OwnerId = currentUser.Id;

                await _repositoryShopping.InsertAsync(shoppingModel);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnderecoId"] = new SelectList(await _repositoryShopping.GetAllAsync(), "EnderecoId", "Rua", shoppingModel.EnderecoId);
            return View(shoppingModel);
        }

        // GET: CidadeModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var shoppingModel = await _repositoryShopping.GetAsync(id.Value);

            if (shoppingModel == null)
                return NotFound();

            var list = await _repositoryEndereco.GetAllAsync();

            ViewData["EnderecoId"] = new SelectList(list, "EnderecoId", "Rua", shoppingModel.EnderecoId);
            return View(shoppingModel);
        }

        // POST: CidadeModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShoppingId,Nome,CNPJ,Email,EnderecoId")] ShoppingModel shoppingModel)
        {
            if (id != shoppingModel.EnderecoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _repositoryShopping.UpdateAsync(id, shoppingModel);

                return RedirectToAction(nameof(Index));
            }
            ViewData["EnderecoId"] = new SelectList(_repositoryEndereco.GetAll(), "EnderecoId", "Rua");
            return View(shoppingModel);
        }

        // GET: CidadeModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingModel = await _repositoryShopping.GetAsync(id);
            if (shoppingModel == null)
            {
                return NotFound();
            }

            return View(shoppingModel);
        }

        // POST: CidadeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repositoryShopping.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
