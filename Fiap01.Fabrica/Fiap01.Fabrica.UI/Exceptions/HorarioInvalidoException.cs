using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap01.Fabrica.UI.Exceptions
{


    [Serializable]
    public class HorarioInvalidoException : Exception
    {
        public HorarioInvalidoException() { }
        public HorarioInvalidoException(string message) : base(message) { }
        public HorarioInvalidoException(string message, Exception inner) : base(message, inner) { }
        protected HorarioInvalidoException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
