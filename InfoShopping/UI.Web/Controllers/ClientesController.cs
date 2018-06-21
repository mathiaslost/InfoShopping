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
    public class ClientesController : Controller
    {
        private readonly Services.IGenericRepository<ClienteModel> _repositoryCliente;
        private readonly Services.IGenericRepository<EnderecoModel> _repositoryEndereco;

        public ClientesController(
            Services.IGenericRepository<ClienteModel> repository,
            Services.IGenericRepository<EnderecoModel> repoEndereco)
        {
            _repositoryCliente = repository;
            _repositoryEndereco = repoEndereco;
        }

        // GET: ClienteModels
        public async Task<IActionResult> Index(int? id)
        {
            var applicationDbContext = await _repositoryCliente
                .GetAllAsync(c => id == null || c.EnderecoId == id, c => c.Endereco);
            return View(applicationDbContext);
        }

        // GET: ClienteModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteModel = await _repositoryCliente.GetAsync(id.Value);
            ViewData["EnderecoId"] = new SelectList(_repositoryEndereco.GetAll(), "EnderecoId", "Rua");

            if (clienteModel == null)
            {
                return NotFound();
            }

            return View(clienteModel);
        }

        // GET: ClienteModels/Create
        public IActionResult Create()
        {
            ViewData["EnderecoId"] = new SelectList(_repositoryEndereco.GetAll(), "EnderecoId", "Rua");
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
                await _repositoryCliente.InsertAsync(clienteModel);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnderecoId"] = new SelectList(await _repositoryCliente.GetAllAsync(), "EnderecoId", "Rua", clienteModel.EnderecoId);
            return View(clienteModel);
        }

        // GET: ClienteModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var clienteModel = await _repositoryCliente.GetAsync(id.Value);

            if (clienteModel == null)
                return NotFound();

            var list = await _repositoryEndereco.GetAllAsync();

            ViewData["EnderecoId"] = new SelectList(list, "EnderecoId", "Rua", clienteModel.EnderecoId);
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
                await _repositoryCliente.UpdateAsync(id, clienteModel);

                return RedirectToAction(nameof(Index));
            }
            ViewData["EnderecoId"] = new SelectList(_repositoryEndereco.GetAll(), "EnderecoId", "Rua");
            return View(clienteModel);
        }

        // GET: ClienteModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteModel = await _repositoryCliente.GetAsync(id);
            if (clienteModel == null)
            {
                return NotFound();
            }

            return View(clienteModel);
        }

        // POST: CidadeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repositoryCliente.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
