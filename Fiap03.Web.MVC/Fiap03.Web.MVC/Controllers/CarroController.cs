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

namespace Fiap03.Web.MVC.Controllers
{
    public class CarroController : Controller
    {

        #region GET

        [HttpGet]
        public ActionResult Cadastrar()
        {
            CarregarMarcas();
            return View();
        }

        [HttpGet]
        public ActionResult Pesquisar(int ano)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DbCarros"].ConnectionString))
            {
                var sql = @"SELECT * FROM Carro AS c INNER JOIN 
                           Documento AS d ON c.Renavam = d.Renavam WHERE c.Ano = @Ano or 0 = @Ano";
                var lista = db
                    .Query<CarroModel, DocumentoModel, CarroModel>(sql,
                        (carro, doc) => { carro.Documento = doc; return carro; }, new { Ano = ano },
                        splitOn: "Renavam, Renavam").ToList();
                return View("Listar", lista);

                ////Pesquisa no banco de dados
                //var sql = "SELECT * FROM Carro WHERE Ano = @Ano or 0 = @Ano";
                //var lista = db.Query<CarroModel>(sql, new { Ano = ano }).ToList();
                ////Retornar para a página de Listar enviando a lista de carros
                //return View("Listar", lista);
            }
        }

        //Abre a tela de edição com o formulário preenchido
        [HttpGet]
        public ActionResult Editar(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            {
                CarregarMarcas();
                var sql = @"SELECT * FROM Carro AS c INNER JOIN 
                           Documento AS d ON c.Renavam = d.Renavam WHERE c.Id = @Id";
                var c = db
                    .Query<CarroModel, DocumentoModel, CarroModel>(sql,
                        (carro, doc) => { carro.Documento = doc; return carro; }, new { Id = id },
                        splitOn: "Renavam, Renavam").FirstOrDefault();
                return View(c);

                ////Buscar o carro no banco pelo id
                //var sql = "SELECT * FROM Carro where Id = @Id";
                //var carro = db.Query<CarroModel>(sql, new { Id = id }).FirstOrDefault();
                //CarregarMarcas();
                ////Mandar o carro para a view
                //return View(carro);
            }
        }

        [HttpGet]
        public ActionResult Listar()
        {
            //envia a lista de carros para a view
            using (IDbConnection connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            {
                var sql = @"SELECT * FROM Carro AS c INNER JOIN 
                           Documento AS d ON c.Renavam = d.Renavam";
                var lista = connection
                    .Query<CarroModel, DocumentoModel, CarroModel>(sql,
                        (carro, doc) => { carro.Documento = doc; return carro; },
                        splitOn: "Renavam, Renavam").ToList();
                return View(lista);
            }
        }

        #endregion

        #region POST

        [HttpPost]
        public ActionResult Editar(CarroModel model)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            {
                using (var txtScope = new TransactionScope())
                {
                    //Cadastra o documento
                    var sqlDoc = @"UPDATE Documento SET DataFabricacao = @DataFabricacao, Categoria = @Categoria
                        WHERE Renavam = @Renavam";

                    db.Execute(sqlDoc, model.Documento);

                    //Cadastra o carro
                    var sql = @"UPDATE Carro SET MarcaId = @MarcaId, 
                        Ano = @Ano, Esportivo = @Esportivo, Placa = @Placa, 
                        Combustivel = @Combustivel, Descricao = @Descricao 
                        WHERE Id = @Id";

                    db.Execute(sql, model);

                    //Completa a transação
                    txtScope.Complete();

                    TempData["msg"] = "Atualizado com sucesso!";
                    return RedirectToAction("Listar");
                }

                //var sql = @"UPDATE Carro SET MarcaId = @MarcaId, 
                //    Ano = @Ano, Esportivo = @Esportivo, Placa = @Placa, 
                //    Combustivel = @Combustivel, Descricao = @Descricao 
                //    WHERE Id = @Id";
                //db.Execute(sql, model);
                //TempData["msg"] = "Atualizado com sucesso!";
                //return RedirectToAction("Listar");
            }
        }

        [HttpPost]
        public ActionResult Excluir(int codigo)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            {
                db.Execute("DELETE FROM Carro WHERE Id = @Id",
                                            new { id = codigo });
                TempData["msg"] = "Carro excluído";
                return RedirectToAction("Listar");
            }
        }

        [HttpPost]
        public ActionResult Cadastrar(CarroModel carro)
        {
            using (IDbConnection db = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            {
                using (var txtScope = new TransactionScope())
                {
                    //Cadastra o documento
                    var sqlDoc = @"INSERT INTO Documento (Renavam, DataFabricacao, 
                        Categoria) VALUES (@Renavam, @DataFabricacao, @Categoria);";

                    db.Execute(sqlDoc, carro.Documento);

                    //Cadastra o carro
                    var sql = @"INSERT INTO Carro (MarcaId, Ano, 
                        Esportivo, Placa, Combustivel, Descricao, Renavam) 
                        VALUES (@MarcaId, @Ano, @Esportivo, @Placa, @Combustivel,
                        @Descricao, @Renavam); SELECT CAST(SCOPE_IDENTITY() as int);";

                    carro.Renavam = carro.Documento.Renavam;
                    int codigo = db.Query<int>(sql, carro).Single();

                    //Completa a transação
                    txtScope.Complete();
                }
            }

            // _carros.Add(carro); //adiciona o carro na lista
            TempData["mensagem"] = "Carro registrado!";
            //Redireciona para uma URL, cria uma segunda request
            //para abrir a página de resposta
            //F5 não cadastra novamente
            return RedirectToAction("Cadastrar");
        }

        #endregion

        private void CarregarMarcas()
        {
            //Listar as marcas do banco de dados
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            {
                var sql = "SELECT * FROM Marca ORDER BY Nome";
                var lista = db.Query<MarcaModel>(sql).ToList();
                ViewBag.marcas = new SelectList(lista, "Id", "Nome");
            }
        }

    }
}