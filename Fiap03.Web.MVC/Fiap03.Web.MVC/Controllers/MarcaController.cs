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

namespace Fiap03.Web.MVC.Controllers
{
    public class MarcaController : Controller
    {
        [HttpGet]
        public ActionResult Detalhar(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DbCarros"].ConnectionString))
            {
                var sql = @"SELECT * FROM Marca WHERE Id = @Id;
                    SELECT * FROM Carro WHERE MarcaId = @Id";
                using (var results = db.QueryMultiple(sql, new { id }))
                {
                    var marca = results.Read<MarcaModel>().SingleOrDefault();
                    var carros = results.Read<CarroModel>().ToList();
                    if (marca != null && carros != null)
                    {
                        marca.Carros = carros;
                    }

                    return View(marca);
                }
            }
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(MarcaModel marca)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DbCarros"].ConnectionString))
            {
                var sql = @"INSERT INTO Marca (Nome, Cnpj, DataCriacao) VALUES (@Nome, @Cnpj, SYSDATETIME());
                            SELECT CAST(SCOPE_IDENTITY() as int);";
                int id = db.Query<int>(sql, marca).Single();
            }
            TempData["msg"] = "Marca registrada!";
            return RedirectToAction("Cadastrar");
        }

        [HttpGet]
        public ActionResult Listar()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DbCarros"].ConnectionString))
            {
                var sql = @"SELECT * FROM Marca";
                var lista = db.Query<MarcaModel>(sql).ToList();
                return View(lista);
            }
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DbCarros"].ConnectionString))
            {
                var sql = @"SELECT * FROM Marca WHERE id = @id";
                var marca = db.Query<MarcaModel>(sql, new { id }).SingleOrDefault();
                return View(marca);
            }
        }


        [HttpPost]
        public ActionResult Editar(MarcaModel marca)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DbCarros"].ConnectionString))
            {
                var sql = @"UPDATE Marca SET Nome = @Nome, Cnpj = @Cnpj WHERE Id = @Id";
                db.Execute(sql, marca);
                TempData["msg"] = "Marca atualizada";
                return RedirectToAction("Listar");
            }
        }

    }
}