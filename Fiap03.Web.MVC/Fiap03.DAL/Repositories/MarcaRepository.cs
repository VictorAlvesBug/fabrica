using Fiap03.DAL.ConnectionFactories;
using Fiap03.DAL.Repositories.Interfaces;
using Fiap03.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Fiap03.DAL.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        public MarcaMOD Buscar(int id)
        {
            using (var db = ConnectionFactory.GetConnection())
            {
                var sql = @"SELECT * FROM Marca WHERE id = @id";
                var marca = db.Query<MarcaMOD>(sql, new { id }).SingleOrDefault();
                return marca;
            }
        }

        public MarcaMOD BuscarComCarros(int id)
        {
            using (var db = ConnectionFactory.GetConnection())
            {
                var sql = @"SELECT * FROM Marca WHERE Id = @Id; 
                      SELECT * FROM Carro WHERE MarcaId = @Id;";
                using (var resultados = db.QueryMultiple(sql, new { Id = id }))
                {
                    var marca = resultados.Read<MarcaMOD>().SingleOrDefault();
                    var carros = resultados.Read<CarroMOD>().ToList();

                    if (marca != null && carros != null)
                    {
                        marca.Carros = carros;
                    }
                    return marca;
                }
            }
        }

        public void Cadastrar(MarcaMOD marca)
        {
            using (var db = ConnectionFactory.GetConnection())
            {
                var sql = @"INSERT INTO Marca (Nome, Cnpj, DataCriacao) VALUES (@Nome, @Cnpj, SYSDATETIME());
                            SELECT CAST(SCOPE_IDENTITY() as int);";
                int id = db.Query<int>(sql, marca).Single();
                marca.Id = id; //associa o id gerado pelo banco
            }
        }

        public void Editar(MarcaMOD marca)
        {
            using (var db = ConnectionFactory.GetConnection())
            {
                var sql = @"UPDATE Marca SET Nome = @Nome, Cnpj = @Cnpj WHERE Id = @Id";
                db.Execute(sql, marca);                
            }
        }

        public IList<MarcaMOD> Listar()
        {
            using(var db = ConnectionFactory.GetConnection())
            {
                var sql = "SELECT * FROM Marca";
                var lista = db.Query<MarcaMOD>(sql).ToList();
                return lista;
            }
        }


    }
}
