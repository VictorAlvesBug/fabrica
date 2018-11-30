using Fiap03.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Transactions;
using Fiap03.DAL.ConnectionFactories;
using Fiap03.DAL.Repositories.Interfaces;
using Fiap03.DAL.Repositories;
using Fiap03.MOD;

namespace Fiap03.Web.MVC.Controllers
{
    public class CarroController : Controller
    {
        private ICarroRepository _carroRepository = new CarroRepository();
        private IMarcaRepository _marcaRepository = new MarcaRepository();
        private IModeloRepository _modeloRepository = new ModeloRepository();

        #region GET
        [HttpGet]
        public ActionResult BuscarModelos(int marcaId)
        {
            //Busca os modelos pelo id da marca
            var listaMod = _modeloRepository.Listar(marcaId);
            //var listaModel = listaMod.Select(m => new ModeloModel(m)).ToList();
            //retorna o JSON array com a lista de modelos
            return Json(listaMod, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ValidarPlaca(string placa)
        {            
            var ok = _carroRepository.ValidarPlaca(placa);
            return Json(new { valido = ok } , JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            CarregarMarcas();
            return View();
        }

        [HttpGet]
        public ActionResult Pesquisar(int ano)
        {
            //Pesquisa os carros no BD
            var listaMod = _carroRepository.BuscarPorAno(ano);
            //Transforma a lista de MOD em Model
            var listaModel = listaMod.Select(c => new CarroModel(c)).ToList();
            //Retorna para a página Listar com a lista de model
            return PartialView("_Lista", listaModel);
        }

        //Abre a tela de edição com o formulário preenchido
        [HttpGet]
        public ActionResult Editar(int id)
        {
            //Pesquisa no banco de dados pelo ID
            var mod = _carroRepository.Buscar(id);
            //Transforma o MOD em MOdel
            var model = new CarroModel(mod);
            //Carregar as marcas para o select
            CarregarMarcas();
            //Retorna para a página com o model
            return View(model);
        }

        [HttpGet]
        public ActionResult Listar()
        {
            //Buscar no banco de dados a lista de mod
            var listaMod = _carroRepository.Listar();
            //transforma a lista de mod em model
            var listaModel = new List<CarroModel>();
            listaMod.ToList().ForEach(c => listaModel.Add(new CarroModel(c)));
            //retorna para a página com a lista de model
            return View(listaModel);
        }

        #endregion

        #region POST

        [HttpPost]
        public ActionResult Editar(CarroModel model)
        {
            if (!ModelState.IsValid)
            {                
                return Editar(model.Id);
            }
            //transforma o model em mod
            CarroMOD mod = ConverterModelParaMOD(model);
            //chama o repository para editar
            _carroRepository.Editar(mod);
            TempData["msg"] = "Atualizado com sucesso!";
            return RedirectToAction("Listar");
        }

        [HttpPost]
        public ActionResult Excluir(int codigo)
        {
            _carroRepository.Excluir(codigo);
            TempData["msg"] = "Carro excluído";
            return RedirectToAction("Listar");
        }

        [HttpPost]
        public ActionResult Cadastrar(CarroModel carro)
        {
            if (!_carroRepository.ValidarPlaca(carro.Placa))
            {
                ModelState.AddModelError("Placa", new Exception("Placa já existente"));
            }
            if (!ModelState.IsValid)
            {
                return Cadastrar();
            }
            var mod = ConverterModelParaMOD(carro);
            _carroRepository.Cadastrar(mod);
            TempData["mensagem"] = "Carro registrado!";
            //Redireciona para uma URL, cria uma segunda request
            //para abrir a página de resposta
            //F5 não cadastra novamente
            return RedirectToAction("Cadastrar");
        }

        #endregion

        private static CarroMOD ConverterModelParaMOD(CarroModel model)
        {
            return new CarroMOD()
            {
                Id = model.Id,
                Ano = model.Ano,
                Combustivel = model.Combustivel,
                Descricao = model.Descricao,
                Esportivo = model.Esportivo,
                MarcaId = model.MarcaId,
                Placa = model.Placa,
                Renavam = model.Renavam,
                Documento = new DocumentoMOD()
                {
                    Categoria = model.Documento.Categoria,
                    Renavam = model.Documento.Renavam,
                    DataFabricacao = model.Documento.DataFabricacao
                }
            };
        }

        private void CarregarMarcas()
        {
            //Listar as marcas do banco de dados
            var lista = _marcaRepository.Listar();
            ViewBag.marcas = new SelectList(lista, "Id", "Nome");
        }

    }
}