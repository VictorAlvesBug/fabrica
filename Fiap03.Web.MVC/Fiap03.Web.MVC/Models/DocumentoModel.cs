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
    }
}