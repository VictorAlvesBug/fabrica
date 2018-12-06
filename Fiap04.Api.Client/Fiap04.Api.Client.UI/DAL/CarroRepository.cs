using Fiap04.Api.Client.UI.DAL.Interfaces;
using Fiap04.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Fiap04.Api.Client.UI.DAL
{
    public class CarroRepository : ICarroRepository
    {
        private string _url = "http://localhost:3646/";

        public void Atualizar(CarroDTO carro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                var resp = client.PutAsJsonAsync("api/carro/" + carro.Id, carro).Result;
                if (!resp.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao atualizar");
                }
            }
        }

        public CarroDTO Buscar(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var resp = client.GetAsync("api/carro/" + id).Result;
                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadAsAsync<CarroDTO>().Result;
                }
                throw new Exception("Erro ao pesquisar");
            }
        }

        public void Cadastrar(CarroDTO carro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                var resp = client.PostAsJsonAsync("api/carro", carro).Result;

                if (!resp.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao cadastrar");
                }
            }
        }

        public IList<CarroDTO> Listar()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                client.DefaultRequestHeaders.Clear();
                //configura o tipo do retorno do webservice (json)
                client.DefaultRequestHeaders
                    .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //faz a chamada no webservice no endpoint api/carro
                var resp = client.GetAsync("api/carro").Result;

                if (resp.IsSuccessStatusCode)
                {
                    var lista = resp.Content.ReadAsAsync<IList<CarroDTO>>().Result;
                    return lista;
                }
                throw new Exception("Erro ao pesquisar");
            }
        }

        public void Remover(int id)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                //chama o ws para remover um carro informando o id na url
                var resp = client.DeleteAsync("api/carro/" + id).Result;
                if (!resp.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao remover");
                }
            }
        }
    }
}
