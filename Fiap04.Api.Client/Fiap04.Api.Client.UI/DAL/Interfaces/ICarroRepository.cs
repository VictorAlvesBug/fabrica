using Fiap04.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap04.Api.Client.UI.DAL.Interfaces
{
    public interface ICarroRepository
    {
        IList<CarroDTO> Listar();
        CarroDTO Buscar(int id);
        void Cadastrar(CarroDTO carro);
        void Atualizar(CarroDTO carro);
        void Remover(int id);
    }
}
