using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UI.Web.Models;

namespace UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly Services.IGenericRepository<EstadoModel> _repositoryEstado;

        public HomeController(Services.IGenericRepository<EstadoModel> repoEstado)
        {
            _repositoryEstado = repoEstado;
        }

        public IActionResult Index()
        {
            ViewData["EstadoId"] = _repositoryEstado.GetAll();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
