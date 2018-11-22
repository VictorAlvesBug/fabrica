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
using Fiap03.DAL.ConnectionFactory;

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
            using (IDbConnection db = ConnectionFactory.GetConnection())
            {
                //Pesquisa no banco de dados
                var sql = @"SELECT * FROM Carro AS c INNER JOIN 
                           Documento AS d ON c.Renavam = d.Renavam WHERE c.Ano = @Ano or 0 = @Ano";
                var lista = db.Query<CarroModel,DocumentoModel,CarroModel>
                    (sql, (carro, doc) => { carro.Documento = doc; return carro; },
                        new { Ano = ano },
                        splitOn: "Renavam, Renavam").ToList();
                //Retornar para a página de Listar enviando a lista de carros
                return View("Listar", lista);
            }
        }

        //Abre a tela de edição com o formulário preenchido
        [HttpGet]
        public ActionResult Editar(int id)
        {
            using (IDbConnection db = ConnectionFactory.GetConnection())
            {
                //Buscar o carro no banco pelo id
                var sql = @"SELECT * FROM Carro AS c INNER JOIN 
                           Documento AS d ON c.Renavam = d.Renavam where c.Id = @Id";
                var carro = db.Query<CarroModel,DocumentoModel,CarroModel>(sql, 
                    (c,doc)=> { c.Documento = doc; return c; },
                    new { Id = id }, splitOn: "Renavam,Renavam").FirstOrDefault();
                CarregarMarcas();
                //Mandar o carro para a view
                return View(carro);
            }
        }

        [HttpGet]
        public ActionResult Listar()
        {
            //envia a lista de carros para a view
            using (IDbConnection connection = ConnectionFactory.GetConnection())
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
            using (IDbConnection db = ConnectionFactory.GetConnection())
            {
                using (var txtScope = new TransactionScope())
                {
                    var sqlDoc = @"UPDATE Documento SET Categoria = @Categoria,
                    DataFabricacao = @DataFabricacao WHERE Renavam = @Renavam";

                    db.Execute(sqlDoc, model.Documento);

                    var sql = @"UPDATE Carro SET MarcaId = @MarcaId, 
                    Ano = @Ano, Esportivo = @Esportivo, Placa = @Placa, 
                    Combustivel = @Combustivel, Descricao = @Descricao 
                    WHERE Id = @Id";

                    db.Execute(sql, model);

                    txtScope.Complete();

                    TempData["msg"] = "Atualizado com sucesso!";
                    return RedirectToAction("Listar");
                }
            }
        }

        [HttpPost]
        public ActionResult Excluir(int codigo)
        {
            using (IDbConnection db = ConnectionFactory.GetConnection())
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
            using (IDbConnection db = ConnectionFactory.GetConnection())
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
            using (IDbConnection db = ConnectionFactory.GetConnection())
            {
                var sql = "SELECT * FROM Marca ORDER BY Nome";
                var lista = db.Query<MarcaModel>(sql).ToList();
                ViewBag.marcas = new SelectList(lista, "Id", "Nome");
            }
        }

    }
}