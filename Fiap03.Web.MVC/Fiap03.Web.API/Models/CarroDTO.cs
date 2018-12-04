using Fiap03.MOD;
using Fiap03.MOD.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap03.Web.API.Models
{
    public class CarroDTO
    {
        public int Id { get; set; }
        public int MarcaId { get; set; }
        public string Placa { get; set; }
        public int Ano { get; set; }
        public bool Esportivo { get; set; }
        public Combustivel? Combustivel { get; set; }
        public string Descricao { get; set; }
        //FK
        public int Renavam { get; set; }
        public DocumentoDTO Documento { get; set; }

        public CarroDTO()
        {

        }

        public CarroDTO(CarroMOD mod)
        {
            Id = mod.Id;
            MarcaId = mod.MarcaId;
            Placa = mod.Placa;
            Ano = mod.Ano;
            Esportivo = mod.Esportivo;
            Combustivel = mod.Combustivel;
            Descricao = mod.Descricao;
            Renavam = mod.Renavam;
            if (mod.Documento != null)
            {
                Documento = new DocumentoDTO(mod.Documento);
            }
        }
    }
}