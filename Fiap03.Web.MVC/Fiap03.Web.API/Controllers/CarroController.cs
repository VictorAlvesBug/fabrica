using Fiap03.DAL.Repositories;
using Fiap03.DAL.Repositories.Interfaces;
using Fiap03.MOD;
using Fiap03.Web.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fiap03.Web.API.Controllers
{
    public class CarroController : ApiController
    {
        private ICarroRepository _rep = new CarroRepository();

        //api/carro/{id}
        public void Delete(int id)
        {
            _rep.Excluir(id);
        }

        //api/carro/{id}
        public IHttpActionResult Put(int id, CarroDTO dto)
        {
            if (ModelState.IsValid)
            {
                dto.Id = id;
                _rep.Editar(Converter(dto));
                return Ok(dto);
            }
            return BadRequest(ModelState);
        }

        //api/carro
        public IHttpActionResult Post(CarroDTO dto)
        {
            if (ModelState.IsValid)
            {
                var mod = Converter(dto);
                _rep.Cadastrar(mod);
                dto.Id = mod.Id;
                var link = Url.Link("DefaultApi", new { id = dto.Id });
                return Created<CarroDTO>(new Uri(link), dto);
            }
            return BadRequest();
        }

        //api/carro/{id}
        public CarroDTO Get(int id)
        {
            return new CarroDTO(_rep.Buscar(id));
        }

        //api/carro
        public IList<CarroDTO> Get()
        {
            return _rep.Listar().Select(c => new CarroDTO(c)).ToList();
        }

        private CarroMOD Converter(CarroDTO dto)
        {
            return new CarroMOD()
            {
                Ano = dto.Ano,
                Combustivel = dto.Combustivel,
                Descricao = dto.Descricao,
                Documento = new DocumentoMOD()
                {
                    Categoria = dto.Documento.Categoria,
                    DataFabricacao = dto.Documento.DataFabricacao,
                    Renavam = dto.Documento.Renavam
                },
                Renavam = dto.Renavam,
                Esportivo = dto.Esportivo,
                Id = dto.Id,
                MarcaId = dto.MarcaId,
                Placa = dto.Placa
            };
        }
    }
}
