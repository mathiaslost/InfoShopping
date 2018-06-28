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
    public class EnderecosController : Controller
    {
        private readonly Services.IGenericRepository<CidadeModel> _repositoryCidade;
        private readonly Services.IGenericRepository<EnderecoModel> _repositoryEndereco;

        public EnderecosController(
            Services.IGenericRepository<EnderecoModel> repository,
            Services.IGenericRepository<CidadeModel> repoCidade)
        {
            _repositoryEndereco = repository;
            _repositoryCidade = repoCidade;
        }

        // GET: EnderecoModels
        public async Task<IActionResult> Index(int? id)
        {
            var applicationDbContext = await _repositoryEndereco
                .GetAllAsync(c => id == null || c.CidadeId == id, c => c.Cidade);
            return View(applicationDbContext);
        }

        // GET: EnderecoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enderecoModel = await _repositoryEndereco.GetAsync(id.Value);
            ViewData["CidadeId"] = new SelectList(_repositoryCidade.GetAll(), "CidadeId", "Nome");

            if (enderecoModel == null)
            {
                return NotFound();
            }

            return View(enderecoModel);
        }

        // GET: EnderecoModels/Create
        public IActionResult Create()
        {
            ViewData["CidadeId"] = new SelectList(_repositoryCidade.GetAll(), "CidadeId", "Nome");
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
                await _repositoryEndereco.InsertAsync(enderecoModel);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CidadeId"] = new SelectList(await _repositoryEndereco.GetAllAsync(), "CidadeId", "Nome", enderecoModel.CidadeId);
            return View(enderecoModel);
        }

        // GET: EnderecoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var enderecoModel = await _repositoryEndereco.GetAsync(id.Value);

            if (enderecoModel == null)
                return NotFound();

            var list = await _repositoryCidade.GetAllAsync();

            ViewData["CidadeId"] = new SelectList(list, "CidadeId", "Nome", enderecoModel.CidadeId);
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
                await _repositoryEndereco.UpdateAsync(id, enderecoModel);

                return RedirectToAction(nameof(Index));
            }
            ViewData["CidadeId"] = new SelectList(_repositoryCidade.GetAll(), "CidadeId", "Nome");
            return View(enderecoModel);
        }

        // GET: EnderecoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enderecoModel = await _repositoryEndereco.GetAsync(id);
            if (enderecoModel == null)
            {
                return NotFound();
            }

            return View(enderecoModel);
        }

        // POST: CidadeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repositoryEndereco.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
