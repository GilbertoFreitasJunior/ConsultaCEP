using ConsultaCEP.Models;
using Correios.NET;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Text.Json;

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

        public string ConsultaCEP(string CEP)
        {
            try
            {
                var enderecos = new Services().GetAddresses(CEP);
                var endereco = new CEP();

                foreach (var item in enderecos)
                {
                    endereco.ZipCode = item.ZipCode;
                    endereco.State = item.State;
                    endereco.City = item.City;
                    endereco.District = item.District;
                    endereco.Street = item.Street;
                }

                return JsonSerializer.Serialize(endereco);
            }
            catch (Exception)
            {
                throw new Exception("Erro ao Consultar o CEP!");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
