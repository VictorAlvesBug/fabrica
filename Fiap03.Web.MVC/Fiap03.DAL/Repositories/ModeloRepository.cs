using Fiap03.DAL.Repositories.Interfaces;
using Fiap03.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Fiap03.DAL.ConnectionFactories;

namespace Fiap03.DAL.Repositories
{
    public class ModeloRepository : IModeloRepository
    {
        public void Cadastrar(ModeloMOD mod)
        {
            using(var db = ConnectionFactory.GetConnection())
            {
                var sql = @"INSERT INTO Modelo (Nome, MarcaId) VALUES (@Nome, @MarcaID);
                    SELECT CAST (SCOPE_IDENTITY() AS INT);";
                int id = db.Query<int>(sql, mod).Single();
                mod.Id = id;
            }
        }

        public IList<ModeloMOD> Listar(int marcaId)
        {
            using(var db = ConnectionFactory.GetConnection())
            {
                var sql = "SELECT * FROM Modelo WHERE MarcaId = @Id";
                var lista = db.Query<ModeloMOD>(sql, new { Id = marcaId }).ToList();
                return lista;
            }
        }
    }
}
