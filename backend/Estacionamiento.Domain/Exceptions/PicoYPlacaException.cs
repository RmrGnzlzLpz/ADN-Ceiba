using System;
using System.Runtime.Serialization;

namespace Estacionamiento.Domain.Exceptions
{
    [Serializable]
    public class PicoYPlacaException : AppException
    {
        public PicoYPlacaException() : this("Vehicle in pico y placa")
        {
        }

        public PicoYPlacaException(string message) : base(message)
        {
        }

        public PicoYPlacaException(string message, Exception inner) : base(message, inner)
        {
        }

        protected PicoYPlacaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
