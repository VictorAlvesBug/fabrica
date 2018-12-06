using Fiap04.Api.Client.UI.DAL;
using Fiap04.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap04.Api.Client.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new CarroRepository();

            //Instanciar o objeto
            var carro = new CarroDTO()
            {
                Ano = 2020,
                Combustivel = Models.Enums.Combustivel.Flex,
                Documento = new DocumentoDTO()
                {
                    Categoria = Models.Enums.Categoria.Sedan,
                    DataFabricacao = new DateTime(2019, 1, 1),
                    Renavam = 5210
                },
                Renavam = 5210,
                Esportivo = false,
                MarcaId = 1,
                Placa = "EDD-1231"
            };

            //Chama o ws para cadastrar o carro
            repository.Cadastrar(carro);

            //chama o web service para listar
            var lista = repository.Listar();

            //imprime o resultado no console
            foreach (var item in lista)
            {
                Console.WriteLine("Placa: {0}, Ano: {1}", 
                    item.Placa, item.Ano);
            }

            Console.Read(); //para a execução...
        }
    }
}
