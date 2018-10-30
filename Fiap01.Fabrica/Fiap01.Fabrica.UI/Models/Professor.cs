using Fiap01.Fabrica.UI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap01.Fabrica.UI.Models
{
    class Professor : Pessoa, IColaborador
    {
        public Professor(string nome) : base(nome) { }

        public void BaterPonto()
        {
            if (DateTime.Now.Hour > 10)
            {
                throw new HorarioInvalidoException("Fora de horario");
            }
            Console.WriteLine("Professor batendo ponto");
        }

        public override void VerBoletim()
        {
            Console.WriteLine("Professor analisando o boletim");
        }
    }
}
