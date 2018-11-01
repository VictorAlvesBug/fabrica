using Fiap02.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiap02.Web.MVC.Controllers
{
    public class AlunoController : Controller
    {
        [HttpGet]
        public ActionResult Listar()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View(new Aluno());
        }

        [HttpPost]
        public ActionResult Cadastrar(Aluno aluno)
        {
            ViewBag.nome = aluno.Nome;
            TempData["msg"] = "Cadastrado com sucesso!";
            return View(aluno);
            //return Content("Nome: " + aluno.Nome +
            //    "Curso: " + aluno.Curso);
        }
    }
}