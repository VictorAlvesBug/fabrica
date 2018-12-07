using System;
using Fiap03.Web.MVC.Controllers;
using Fiap03.Web.MVC.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fiap03.Web.MVC.Test
{
    [TestClass]
    public class MarcaControllerTest
    {
        [TestMethod]
        public void Cadastro_Post_Test()
        {
            //Criar o objeto Controller
            MarcaController controller = new MarcaController();
            //Criar um objeto Marca
            MarcaModel model = new MarcaModel()
            {
                Cnpj = "123456646",
                Nome = "Marca teste"
            };
            //Chamar o método cadastrar POST
            controller.Cadastrar(model);
            //Validar se o resultado tem a mensagem de sucesso
            Assert.AreEqual(controller.TempData["msg"],
                "Marca registrada!");
        }
    }
}
