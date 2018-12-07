using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fiap03.Web.MVC.ViewModel
{
    public class UsuarioViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Senha { get; set; }

        public string Url { get; set; }
    }
}