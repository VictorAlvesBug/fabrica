using Fiap03.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using Fiap03.DAL.Repositories.Interfaces;
using Fiap03.DAL.Repositories;
using Fiap03.MOD;

namespace Fiap03.Web.MVC.Controllers
{
    public class MarcaController : Controller
    {
        private IMarcaRepository _marcaRepository = new MarcaRepository();

        [HttpGet]
        public ActionResult Detalhar(int id)
        {
            //pesquisa a marca com os carros no BD
            var mod = _marcaRepository.BuscarComCarros(id);
            //transformar o mod em model
            var model = new MarcaModel(mod);
            return View(model);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(MarcaModel marca)
        {
            //Valida os campos do formulário
            if (!ModelState.IsValid)
            {
                //Erro de validação, retorna para a página
                return View(marca); 
            }
            //Transformar de model para mod
            var mod = new MarcaMOD()
            {
                Cnpj = marca.Cnpj,
                DataCriacao = marca.DataCriacao,
                Nome = marca.Nome
            };

            try
            {
                //Chamar o repository (cadastrar) para gravar no BD
                _marcaRepository.Cadastrar(mod);
                TempData["msg"] = "Marca registrada!";
                return RedirectToAction("Cadastrar");
            }
            catch
            {
                TempData["msg"] = "Erro, por favor tente mais tarde";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Listar()
        {
            //Instanciar uma lista de marcaModel
            var listaModel = new List<MarcaModel>();
            //Buscar as marcaMOD do banco de dados
            var listaMod = _marcaRepository.Listar();
            //Converter o MOD para Model
            listaMod.ToList().ForEach(
                c => listaModel.Add(new MarcaModel(c)));
            //Retornar a View com a lsita de marcaModel
            return View(listaModel);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            //Buscar a marcaMOD do banco de dados pelo ID
            var mod = _marcaRepository.Buscar(id);
            //Transformar o MOD para Model
            var model = new MarcaModel(mod);
            //Retornar a View com o Model
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(MarcaModel marca)
        {
            //Transformar o model para mod
            var mod = new MarcaMOD()
            {
                Cnpj = marca.Cnpj,
                Id = marca.Id,
                Nome = marca.Nome
            };
            //Chamar o método do repository para editar
            _marcaRepository.Editar(mod);
            TempData["msg"] = "Marca atualizada";
            return RedirectToAction("Listar");
        }
    }
}