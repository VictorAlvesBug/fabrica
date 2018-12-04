using Fiap04.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap04.Models
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

    }
}