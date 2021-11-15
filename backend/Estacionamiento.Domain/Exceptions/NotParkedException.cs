using System;
using System.Runtime.Serialization;

namespace Estacionamiento.Domain.Exceptions
{
    [Serializable]
    public class NotParkedException : AppException
    {
        public NotParkedException() : this("Vehicle is not parked")
        {
        }

        public NotParkedException(string message) : base(message)
        {
        }

        public NotParkedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NotParkedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
