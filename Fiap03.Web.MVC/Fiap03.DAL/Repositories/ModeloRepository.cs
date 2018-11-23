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
                
            }
        }

        public IList<ModeloMOD> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
