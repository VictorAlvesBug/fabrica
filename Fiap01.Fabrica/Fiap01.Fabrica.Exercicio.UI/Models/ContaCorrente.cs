using Fiap01.Fabrica.Exercicio.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Banco.Model
{
    //sealed -> não permite a herança
    public sealed class ContaCorrente : Conta
    {
        public TipoConta Tipo { get; set; }

        public override void Retirar(decimal valor)
        {
            if (Tipo == TipoConta.Comum && Saldo-valor < 0)
            {
                throw new SaldoInsuficienteException("Saldo insuficiente");
            }
            Saldo -= valor;
        }
    }
}
