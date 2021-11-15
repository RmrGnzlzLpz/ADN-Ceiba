using System;
using System.Runtime.Serialization;

namespace Estacionamiento.Domain.Exceptions
{
    [Serializable]
    public class AlreadyParkedException : AppException
    {
        public AlreadyParkedException() : this("Vehicle already parked")
        {
        }

        public AlreadyParkedException(string message) : base(message)
        {
        }

        public AlreadyParkedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected AlreadyParkedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
