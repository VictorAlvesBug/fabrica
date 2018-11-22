using Fiap03.MOD;
using Fiap03.MOD.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fiap03.Web.MVC.Models
{
    public class DocumentoModel
    {
        public int Renavam { get; set; }

        public Categoria? Categoria { get; set; }

        [Display(Name = "Data de Fabricação")]
        public DateTime DataFabricacao { get; set; }
        
        //Construtor
        public DocumentoModel(DocumentoMOD mod)
        {
            Renavam = mod.Renavam;
            Categoria = mod.Categoria;
            DataFabricacao = mod.DataFabricacao;
        }

        public DocumentoModel()
        {

        }
    }
}