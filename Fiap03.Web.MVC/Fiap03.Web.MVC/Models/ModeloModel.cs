using Fiap03.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap03.Web.MVC.Models
{
    public class ModeloModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int MarcaId { get; set; }

        public ModeloModel(){ }

        public ModeloModel(ModeloMOD mod)
        {
            Id = mod.Id;
            Nome = mod.Nome;
            MarcaId = mod.MarcaId;
        }
    }
}