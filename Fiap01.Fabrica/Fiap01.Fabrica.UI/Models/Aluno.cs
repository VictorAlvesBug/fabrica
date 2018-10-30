using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap01.Fabrica.UI.Models
{


    public class Aluno : Pessoa
    {
        //Propriedade
        public string Rm { get; set; }

        //Construtor
        public Aluno(string rm, string nome) : base(nome)
        {
            Rm = rm;
        }

        public override void VerBoletim()
        {
            Console.WriteLine("Aluno visualizando o boletim");
        }

        public override void Cadastrar()
        {
            Console.WriteLine("Aluno se cadastrando");
        }
    }
}
