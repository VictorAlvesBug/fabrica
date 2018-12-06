using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiap03.Web.MVC.Controllers
{
    public class ErroController : Controller
    {        
        public ActionResult Padrao()
        {
            return View();
        }

        public ActionResult NaoEncontrado()
        {
            return View();
        }
    }
}