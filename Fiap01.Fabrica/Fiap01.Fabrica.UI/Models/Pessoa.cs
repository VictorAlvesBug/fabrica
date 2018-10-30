using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap01.Fabrica.UI.Models
{
    public abstract class Pessoa
    {
        private string _nome;

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public int Idade { get; set; }

        public Genero Sexo { get; set; }

        public Pessoa(string nome)
        {
            Nome = nome;
        }

        public abstract void VerBoletim();

        //virtual -> permite a sobrescrita do método
        public virtual void Cadastrar()
        {
            Console.WriteLine("Pessoa se cadastrando");
        }
        
    }
}
