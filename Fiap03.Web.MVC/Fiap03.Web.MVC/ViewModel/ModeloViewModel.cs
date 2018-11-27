using Fiap03.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap03.Web.MVC.ViewModel
{
    public class ModeloViewModel
    {
        public ModeloModel Modelo { get; set; }
        public IList<ModeloModel> Lista { get; set; }
        public string NomeMarca { get; set; }
    }
}