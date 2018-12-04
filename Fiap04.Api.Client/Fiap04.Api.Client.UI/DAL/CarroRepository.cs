using Fiap04.Api.Client.UI.DAL.Interfaces;
using Fiap04.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fiap04.Api.Client.UI.DAL
{
    public class CarroRepository : ICarroRepository
    {
        public void Atualizar(CarroDTO carro)
        {
            throw new NotImplementedException();
        }

        public CarroDTO Buscar(int id)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(CarroDTO carro)
        {
            throw new NotImplementedException();
        }

        public IList<CarroDTO> Listar()
        {
            using (var client = new HttpClient())
            {

            }
        }

        public void Remover(int id)
        {
            throw new NotImplementedException();
        }
    }
}
