using Fiap03.DAL.Repositories;
using Fiap03.DAL.Repositories.Interfaces;
using Fiap03.MOD;
using Fiap03.Web.MVC.Models;
using Fiap03.Web.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiap03.Web.MVC.Controllers
{
    public class ModeloController : Controller
    {
        private IModeloRepository _repModelo = new ModeloRepository();
        private IMarcaRepository _repMarca = new MarcaRepository();

        [HttpGet]
        public ActionResult Index(int id)
        {
            //pesquisa a marca
            var marca = new MarcaModel(_repMarca.Buscar(id));
            //instancia a view model com as informações para a tela
            var viewModel = new ModeloViewModel()
            {
                NomeMarca = marca.Nome,
                Lista = _repModelo.Listar(id).Select(c => new ModeloModel(c)).ToList(),
                Modelo = new ModeloModel()
                {
                    MarcaId = marca.Id
                }
            };
            //envia o view model para a tela
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Cadastrar(ModeloViewModel model)
        {
            var mod = new ModeloMOD()
            {
                MarcaId = model.Modelo.MarcaId,
                Nome = model.Modelo.Nome
            };
            _repModelo.Cadastrar(mod);
            TempData["msg"] = "Cadastrado com sucesso!";
            return RedirectToAction("Index", new { id = mod.MarcaId });
        }
    }
}