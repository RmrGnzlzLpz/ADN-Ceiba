using System;
using System.Runtime.Serialization;

namespace Estacionamiento.Domain.Exceptions
{
    [Serializable]
    public class NoFreeCellsException : AppException
    {
        public NoFreeCellsException() : this("No cells free to park")
        {
        }

        public NoFreeCellsException(string message) : base(message)
        {
        }

        public NoFreeCellsException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NoFreeCellsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
