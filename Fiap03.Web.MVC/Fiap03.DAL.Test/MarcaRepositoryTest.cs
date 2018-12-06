using System;
using System.Data.SqlClient;
using Fiap03.DAL.Repositories;
using Fiap03.DAL.Repositories.Interfaces;
using Fiap03.MOD;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fiap03.DAL.Test
{
    [TestClass]
    public class MarcaRepositoryTest
    {
        private IMarcaRepository rep;

        //método que executa antes dos testes
        [TestInitialize]
        public void init()
        {
            rep = new MarcaRepository();
        }

        [TestMethod]
        public void Cadastro_Marca_Test()
        {
            //cria o objeto marca e chama o método para cadastra-lo
            var marca = new MarcaMOD()
            {
                Cnpj = "55555555555",
                Nome = "Teste",
                DataCriacao = new DateTime(2010, 1,19)
            };
            rep.Cadastrar(marca);
            //valida se deu ok - foi gerado um id pelo BD
            Assert.IsNotNull(marca.Id);
            Assert.AreNotEqual(0, marca.Id);
        }

        [TestMethod]
        public void Lista_Marca_Test()
        {            
            //chamar o método que será testado
            var lista = rep.Listar();
            //validar se está ok
            Assert.IsNotNull(lista);
            //A quantidade de elementos da lista não é 0
            Assert.AreNotEqual(0, lista.Count);
        }

        //teste passa se o método lançar a execeção
        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void Cadastrar_Sem_Nome_Test()
        {
            var marca = new MarcaMOD()
            {
                Cnpj = "44444444"                
            };
            rep.Cadastrar(marca);
        }
    }
}
