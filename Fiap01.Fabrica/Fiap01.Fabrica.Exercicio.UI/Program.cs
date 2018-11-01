using Fiap.Banco.Model;
using Fiap01.Fabrica.Exercicio.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap01.Fabrica.Exercicio.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var cc = new ContaCorrente()
            {
                Agencia = 123,
                Saldo = 100,
                DataAbertura = new DateTime(2010, 10, 20),
                Tipo = TipoConta.Comum
            };
            var cp = new ContaPoupanca(0.05m)
            {
                Agencia = 123,
                Numero = 321,
                Saldo = 200,
                DataAbertura = DateTime.Now
            };
            try
            {
                cc.Retirar(200);
            }
            catch(SaldoInsuficienteException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.Read();
        }
    }
}
