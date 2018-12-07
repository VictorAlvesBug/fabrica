using Fiap03.Web.MVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap03.Web.MVC.Contexts
{
    public class UsuarioContext : IdentityDbContext<UsuarioModel>
    {
        public UsuarioContext():base("UsuarioContext")
        {

        }
    }
}