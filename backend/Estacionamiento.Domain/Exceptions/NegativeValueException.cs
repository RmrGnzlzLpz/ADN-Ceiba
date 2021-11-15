using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamiento.Domain.Exceptions
{
    [Serializable]
    public class NegativeValueException : AppException
    {
        public NegativeValueException() : this("Value cannot negative")
        {
        }

        public NegativeValueException(string message) : base(message)
        {
        }

        public NegativeValueException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NegativeValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
