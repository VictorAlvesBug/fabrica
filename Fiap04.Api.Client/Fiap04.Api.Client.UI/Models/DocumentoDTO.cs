using Fiap04.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap04.Models
{
    public class DocumentoDTO
    {
        public int Renavam { get; set; }

        public Categoria? Categoria { get; set; }

        public DateTime DataFabricacao { get; set; }

        public DocumentoDTO()
        {

        }
    }
}