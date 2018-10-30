using Fiap01.Fabrica.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap01.Fabrica.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Objeto aluno
            Aluno aluno = new Aluno("123","Thiago")
            {                
                Idade = 10
            };

            aluno.Sexo = Genero.Masculino;

            //Criar uma lista de alunos
            IList<Aluno> turma = new List<Aluno>();
            turma.Add(aluno);
            turma.Add(new Aluno("321", "Arnaldo"));

            foreach (var item in turma)
            {
                Console.WriteLine(item.Nome);
            }

        }
    }
}
