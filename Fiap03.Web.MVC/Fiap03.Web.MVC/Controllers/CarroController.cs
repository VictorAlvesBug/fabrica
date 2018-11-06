using Fiap03.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiap03.Web.MVC.Controllers
{
    public class CarroController : Controller
    {
        //Simular o BD
        private static IList<CarroModel> _carros = new List<CarroModel>();

        private IList<String> _marcas = new List<String>()
            { "Hyundai" , "Ferrari" , "Jeep" };

        [HttpGet]
        public ActionResult Cadastrar()
        {            
            ViewBag.marcas = new SelectList(_marcas);
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(CarroModel carro)
        {
            _carros.Add(carro); //adiciona o carro na lista
            TempData["mensagem"] = "Carro registrado!";
            //Redireciona para uma URL, cria uma segunda request
            //para abrir a página de resposta
            //F5 não cadastra novamente
            return RedirectToAction("Cadastrar");
        }

        [HttpGet]
        public ActionResult Listar()
        {
            //envia a lista de carros para a view
            return View(_carros);
        }
    }
}