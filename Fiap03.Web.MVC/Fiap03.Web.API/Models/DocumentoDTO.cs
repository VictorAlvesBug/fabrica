using Fiap03.MOD;
using Fiap03.MOD.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap03.Web.API.Models
{
    public class DocumentoDTO
    {
        public int Renavam { get; set; }

        public Categoria? Categoria { get; set; }

        public DateTime DataFabricacao { get; set; }

        public DocumentoDTO()
        {

        }

        public DocumentoDTO(DocumentoMOD mod)
        {
            Renavam = mod.Renavam;
            Categoria = mod.Categoria;
            DataFabricacao = mod.DataFabricacao;
        }
    }
}