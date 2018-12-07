using Fiap03.Web.MVC.App_Start;
using Fiap03.Web.MVC.Models;
using Fiap03.Web.MVC.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Fiap03.Web.MVC.Controllers
{
    public class UsuarioController : Controller
    {
        //gerenciador de usuário
        private readonly UserManager<UsuarioModel> _userManager;

        [HttpGet]
        public ActionResult Logout()
        {
            GetAuthenticationManager().SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //login para o usuário
        [HttpGet]
        public ActionResult Login(string ReturnUrl)
        {
            //capturar a URL que o usuário está tentando acessar
            var model = new UsuarioViewModel
            {
                Url = ReturnUrl
            };
            return View(model); //envia a model com a url para a view
        }

        [HttpPost]
        public async Task<ActionResult> Login(UsuarioViewModel model)
        {
            if (!ModelState.IsValid) //valida os campos do formulário
            {
                return View();
            }

            //Busca o usuário no banco de dados
            var usuario = await _userManager.FindAsync(model.Login, model.Senha);

            //valida se encontrou o usuário
            if (usuario != null)
            {
                var identity = await _userManager.CreateIdentityAsync(usuario, 
                    DefaultAuthenticationTypes.ApplicationCookie);
                GetAuthenticationManager().SignIn(identity);
                return Redirect(GetRedirectUrl(model.Url));
            }

            //não encontrou o usuário
            ModelState.AddModelError("", "Usuário e/ou senha inválido");
            return View();
        }

        //cadastrar um usuário
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(UsuarioViewModel model)
        {
            if (!ModelState.IsValid) //valida se o model está ok
            {
                return View();
            }

            //criar um objeto para persistir no banco de dados
            var usuario = new UsuarioModel
            {
                UserName = model.Login
            };

            //persisti no banco de dados
            var result = await _userManager.CreateAsync(usuario, model.Senha);

            //verifica se cadastrou com sucesso
            if (result.Succeeded)
            {
                //loga o usuário no sistema
                var identity = await _userManager.CreateIdentityAsync(usuario, DefaultAuthenticationTypes.ApplicationCookie);
                GetAuthenticationManager().SignIn(identity);
                //redireciona para a página inicial
                return RedirectToAction("index", "home");
            }

            //Se der erro ao cadastrar o usuário, adiciona os erros para a view
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item);
            }
            return View();
        }

        //construtor para inicializar o gerenciado de usuário
        public UsuarioController()
        {
            _userManager = IdentityConfig.UserManagerFactory.Invoke();
        }

        //liberar os recursos quando o objeto controller for destruido
        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
            }
            base.Dispose(disposing);
        }

        private IAuthenticationManager GetAuthenticationManager()
        {
            var ctx = Request.GetOwinContext();
            return ctx.Authentication;
        }
        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }
            return returnUrl;
        }
    }
}