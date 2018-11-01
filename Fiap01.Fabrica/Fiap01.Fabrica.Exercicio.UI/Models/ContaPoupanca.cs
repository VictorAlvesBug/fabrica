using Fiap01.Fabrica.Exercicio.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Banco.Model
{
    public class ContaPoupanca : Conta, IContaInvestimento
    {
        private readonly decimal _rendimento;

        public decimal Taxa { get; set; }

        public ContaPoupanca(decimal rendimento)
        {
            _rendimento = rendimento;            
        }

        public decimal CalculaRetornoInvestimento()
        {
            return Saldo * _rendimento;
        }

        public override void Retirar(decimal valor)
        {
            if (Saldo-(valor+Taxa) < 0)
            {
                throw new SaldoInsuficienteException();
            }
            Saldo -= (valor + Taxa);
        }
    }
}
