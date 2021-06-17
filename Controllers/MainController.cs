using ConsultaCEP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Correios.NET;

namespace ConsultaCEP.Controllers
{
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;

        public MainController(ILogger<MainController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ConsultaCEP(string CEP)
        {
            var enderecos = new Services().GetAddresses(CEP);
            var endereco = new CEP();

            foreach (var item in enderecos)
            {
                endereco.ZipCode = item.ZipCode;
                endereco.District = item.District;
                endereco.State = item.State;
                endereco.City = item.City;
                endereco.Street = item.Street;
            }

            ViewData["Endereco"] = endereco;
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
