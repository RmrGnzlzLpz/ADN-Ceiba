using System;
using System.Runtime.Serialization;

namespace Estacionamiento.Domain.Exceptions
{
    [Serializable]
    public class NotFoundException : AppException
    {
        public NotFoundException() : this("Vehicle not found")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
