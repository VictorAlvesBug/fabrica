using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap02.Web.MVC.Models
{
    public class Aluno
    {
        public string Nome { get; set; }
        public string Curso { get; set; }
        public string Periodo { get; set; }
        public bool Funcionario { get; set; }
        public string Descricao { get; set; }
    }
}