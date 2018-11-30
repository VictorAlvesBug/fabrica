using Fiap03.DAL.ConnectionFactories;
using Fiap03.DAL.Repositories.Interfaces;
using Fiap03.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;

namespace Fiap03.DAL.Repositories
{
    public class CarroRepository : ICarroRepository
    {
        public CarroMOD Buscar(int id)
        {
            using (var db = ConnectionFactory.GetConnection())
            {
                //Buscar o carro no banco pelo id
                var sql = @"SELECT * FROM Carro AS c INNER JOIN 
                           Documento AS d ON c.Renavam = d.Renavam where c.Id = @Id";
                var carro = db.Query<CarroMOD, DocumentoMOD, CarroMOD>(sql,
                    (c, doc) => { c.Documento = doc; return c; },
                    new { Id = id }, splitOn: "Renavam,Renavam").FirstOrDefault();
                return carro;
            }
        }

        public IList<CarroMOD> BuscarPorAno(int ano)
        {
            using (var db = ConnectionFactory.GetConnection())
            {
                var sql = @"SELECT * FROM Carro AS c INNER JOIN 
                           Documento AS d ON c.Renavam = d.Renavam WHERE c.Ano = @Ano or 0 = @Ano";
                var lista = db.Query<CarroMOD, DocumentoMOD, CarroMOD>
                    (sql, (carro, doc) => { carro.Documento = doc; return carro; },
                        new { Ano = ano },
                        splitOn: "Renavam, Renavam").ToList();
                return lista;
            }
        }

        public void Cadastrar(CarroMOD mod)
        {
            using (var db = ConnectionFactory.GetConnection())
            {
                using (var txtScope = new TransactionScope())
                {
                    //Cadastra o documento
                    var sqlDoc = @"INSERT INTO Documento (Renavam, DataFabricacao, 
                        Categoria) VALUES (@Renavam, @DataFabricacao, @Categoria);";

                    db.Execute(sqlDoc, mod.Documento);

                    //Cadastra o carro
                    var sql = @"INSERT INTO Carro (MarcaId, Ano, 
                        Esportivo, Placa, Combustivel, Descricao, Renavam) 
                        VALUES (@MarcaId, @Ano, @Esportivo, @Placa, @Combustivel,
                        @Descricao, @Renavam); SELECT CAST(SCOPE_IDENTITY() as int);";

                    mod.Renavam = mod.Documento.Renavam;
                    int codigo = db.Query<int>(sql, mod).Single();

                    //Completa a transação
                    txtScope.Complete();
                }
            }
        }

        public void Editar(CarroMOD mod)
        {
            using (var db = ConnectionFactory.GetConnection())
            {
                using (var txtScope = new TransactionScope())
                {

                    //Cadastra o documento
                    var sqlDoc = @"UPDATE Documento SET DataFabricacao = @DataFabricacao, Categoria = @Categoria
                        WHERE Renavam = @Renavam";

                    db.Execute(sqlDoc, mod.Documento);

                    //Cadastra o carro
                    var sql = @"UPDATE Carro SET MarcaId = @MarcaId, 
                        Ano = @Ano, Esportivo = @Esportivo, Placa = @Placa, 
                        Combustivel = @Combustivel, Descricao = @Descricao 
                        WHERE Id = @Id";

                    db.Execute(sql, mod);

                    //Completa a transação
                    txtScope.Complete();
                }
            }
        }

        public void Excluir(int id)
        {
            using (var db = ConnectionFactory.GetConnection())
            {
                db.Execute("DELETE FROM Carro WHERE Id = @Id", new { Id = id });
            }
        }

        public IList<CarroMOD> Listar()
        {
            //envia a lista de carros para a view
            using (var connection = ConnectionFactory.GetConnection())
            {
                var sql = @"SELECT * FROM Carro AS c INNER JOIN 
                           Documento AS d ON c.Renavam = d.Renavam";
                var lista = connection
                    .Query<CarroMOD, DocumentoMOD, CarroMOD>(sql,
                        (carro, doc) => { carro.Documento = doc; return carro; },
                        splitOn: "Renavam, Renavam").ToList();
                return lista;
            }
        }

        public bool ValidarPlaca(string placa)
        {
            //true -> não encontra a placa no DB
            //false -> a placa existe no banco de dados
            using (var db = ConnectionFactory.GetConnection())
            {
                var sql = "SELECT COUNT(*) FROM Carro WHERE Placa = @Placa";
                var retorno = db.Query<int>(sql, new { Placa = placa }).Single();
                return retorno == 0;
            }
        }
    }
}
