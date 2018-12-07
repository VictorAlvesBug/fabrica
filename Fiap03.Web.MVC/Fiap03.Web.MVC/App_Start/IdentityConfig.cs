using Fiap03.Web.MVC.Contexts;
using Fiap03.Web.MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap03.Web.MVC.App_Start
{
    public class IdentityConfig
    {
        //Gerencia o usuário para logar e deslogar
        public static Func<UserManager<UsuarioModel>> UserManagerFactory { get; set; }

        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                //Utiliza cookies para manter a sessão
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //Página para realizar o login
                LoginPath = new PathString("/usuario/login")
            });
            //Cria a fábrica de gerenciador de usuários
            UserManagerFactory = () =>
            {
                var usermanager = new UserManager<UsuarioModel>(
                new UserStore<UsuarioModel>(new UsuarioContext()));
                // permite caracteres alfa numéricos no username
                usermanager.UserValidator = new UserValidator<UsuarioModel>(usermanager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };
                return usermanager;
            };
        }


    }
}