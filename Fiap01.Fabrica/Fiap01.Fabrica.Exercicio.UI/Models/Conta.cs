﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Banco.Model
{
    public abstract class Conta
    {
        public int Agencia { get; set; }
        public int Numero { get; set; }
        public DateTime DataAbertura { get; set; }
        public decimal Saldo { get; set; }

        public abstract void Retirar(decimal valor);

        public virtual void Depositar(decimal valor)
        {
            Saldo += valor;
        }
    }
}
